import React, { useState, useEffect } from 'react';
import { BookOpen, Package, Users, Plus, Edit2, Trash2, Search } from 'lucide-react';

const API_BASE_URL = process.env.REACT_APP_API_URL + '/api';

// Configs for tables & forms 
const tableColumns = {
    books: ['title', 'isbn', 'genre', 'price', 'stockQuantity', 'publisherName'],
    publishers: ['name', 'country', 'email'],
    orders: ['orderId', 'bookTitle', 'quantity', 'orderDate', 'orderMethod', 'status']
};

const formFields = {
    books: [
        { name: 'title', label: 'Title', type: 'text', required: true },
        { name: 'isbn', label: 'ISBN', type: 'text' },
        { name: 'genre', label: 'Genre', type: 'text' },
        { name: 'price', label: 'Price', type: 'number', required: true },
        { name: 'stockQuantity', label: 'Stock Quantity', type: 'number', required: true },
        { name: 'publisherId', label: 'Publisher', type: 'select', optionsKey: 'publishers', required: true },
        { name: 'publicationYear', label: 'Publication Year', type: 'number', required: true }
    ],
    publishers: [
        { name: 'name', label: 'Name', type: 'text', required: true },
        { name: 'country', label: 'Country', type: 'text', required: true },
        { name: 'email', label: 'Email', type: 'email', required: true }
    ],
    orders: [
        { name: 'bookId', label: 'Book', type: 'select', optionsKey: 'books', required: true },
        { name: 'quantity', label: 'Quantity', type: 'number', required: true },
        { name: 'orderDate', label: 'Order Date', type: 'date', required: true },
        { name: 'orderMethod', label: 'Order Method', type: 'select', options: ['Online', 'In-Store', 'Phone'], required: true },
        { name: 'status', label: 'Status', type: 'select', options: ['Pending', 'Processing', 'Completed', 'Cancelled'], required: true }
    ]
};

// Generic API helper 
const apiRequest = async (endpoint, method = 'GET', body) => {
    const res = await fetch(`${API_BASE_URL}/${endpoint}`, {
        method,
        headers: body ? { 'Content-Type': 'application/json' } : undefined,
        body: body ? JSON.stringify(body) : undefined
    });
    if (!res.ok) throw new Error(`API request failed: ${res.status}`);
    return method === 'DELETE' ? null : res.json();
};

export default function BookstoreApp() {
    const [activeTab, setActiveTab] = useState('books');
    const [data, setData] = useState({ books: [], publishers: [], orders: [] });
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState('');
    const [searchTerm, setSearchTerm] = useState('');
    const [showModal, setShowModal] = useState(false);
    const [modalMode, setModalMode] = useState('create');
    const [currentItem, setCurrentItem] = useState(null);

    useEffect(() => { fetchData(); }, [activeTab]);

    const getEndpoint = () => activeTab;

    const fetchData = async () => {
        setLoading(true); setError('');
        try {
            const items = await apiRequest(getEndpoint());
            setData(prev => ({ ...prev, [activeTab]: items }));
        } catch (err) {
            console.error(err); setError('Failed to fetch data. Make sure the API is running.');
        } finally { setLoading(false); }
    };

    const handleDelete = async (id) => {
        if (!window.confirm('Are you sure you want to delete this item?')) return;
        try {
            await apiRequest(`${getEndpoint()}/${id}`, 'DELETE');
            fetchData();
        } catch (err) { console.error(err); setError('Failed to delete item'); }
    };

    const handleSubmit = async (formData) => {
        try {
            const endpoint = getEndpoint();
            const method = modalMode === 'create' ? 'POST' : 'PUT';
            const id = currentItem?.bookId || currentItem?.publisherId || currentItem?.orderId;
            await apiRequest(modalMode === 'create' ? endpoint : `${endpoint}/${id}`, method, formData);
            setShowModal(false); setCurrentItem(null); fetchData();
        } catch (err) { console.error(err); setError('Failed to save item'); }
    };

    const filteredData = () => {
        const items = data[activeTab] || [];
        if (!searchTerm) return items;
        const fields = tableColumns[activeTab];
        return items.filter(item => fields.some(f => item[f]?.toString().toLowerCase().includes(searchTerm.toLowerCase())));
    };

    return (
        <div className="min-h-screen bg-gray-50">
            <Header />
            <div className="container mx-auto px-4 py-8">
                {error && <Alert message={error} />}
                <Tabs activeTab={activeTab} setActiveTab={setActiveTab} />
                <div className="bg-white rounded-lg shadow-md p-6">
                    <Toolbar searchTerm={searchTerm} setSearchTerm={setSearchTerm} onAdd={() => { setModalMode('create'); setCurrentItem(null); setShowModal(true); }} />
                    {loading ? <Spinner /> : <DataTable data={filteredData()} type={activeTab} onEdit={item => { setModalMode('edit'); setCurrentItem(item); setShowModal(true); }} onDelete={handleDelete} />}
                </div>
            </div>
            {showModal && <Modal type={activeTab} mode={modalMode} item={currentItem} data={data} onClose={() => setShowModal(false)} onSubmit={handleSubmit} />}
        </div>
    );
}

//  Components 
const Header = () => (
    <header className="bg-indigo-600 text-white shadow-lg">
        <div className="container mx-auto px-4 py-6">
            <h1 className="text-3xl font-bold flex items-center gap-3">
                <BookOpen size={32} /> Bookstore Management System
            </h1>
        </div>
    </header>
);

const Alert = ({ message }) => (
    <div className="bg-red-100 border border-red-400 text-red-700 px-4 py-3 rounded mb-4">{message}</div>
);

const Tabs = ({ activeTab, setActiveTab }) => {
    const tabs = [
        { name: 'books', icon: <BookOpen size={20} /> },
        { name: 'publishers', icon: <Users size={20} /> },
        { name: 'orders', icon: <Package size={20} /> }
    ];
    return (
        <div className="bg-white rounded-lg shadow-md mb-6">
            <div className="flex border-b">
                {tabs.map(tab => (
                    <button key={tab.name} onClick={() => setActiveTab(tab.name)}
                        className={`flex-1 px-6 py-4 font-semibold flex items-center justify-center gap-2 ${activeTab === tab.name ? 'bg-indigo-50 text-indigo-600 border-b-2 border-indigo-600' : 'text-gray-600 hover:bg-gray-50'}`}>
                        {tab.icon}{tab.name.charAt(0).toUpperCase() + tab.name.slice(1)}
                    </button>
                ))}
            </div>
        </div>
    );
};

const Toolbar = ({ searchTerm, setSearchTerm, onAdd }) => (
    <div className="flex justify-between items-center mb-6">
        <div className="relative flex-1 max-w-md">
            <Search className="absolute left-3 top-1/2 transform -translate-y-1/2 text-gray-400" size={20} />
            <input type="text" placeholder="Search..." value={searchTerm} onChange={e => setSearchTerm(e.target.value)}
                className="w-full pl-10 pr-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500 focus:border-transparent" />
        </div>
        <button onClick={onAdd} className="ml-4 bg-indigo-600 text-white px-6 py-2 rounded-lg hover:bg-indigo-700 flex items-center gap-2 font-semibold"><Plus size={20} />Add New</button>
    </div>
);

const Spinner = () => (
    <div className="text-center py-12">
        <div className="inline-block animate-spin rounded-full h-12 w-12 border-b-2 border-indigo-600"></div>
    </div>
);

//  Generic DataTable 
function DataTable({ data, type, onEdit, onDelete }) {
    if (data.length === 0) return <div className="text-center text-gray-500 py-12">No data found</div>;

    const columns = tableColumns[type];
    return (
        <div className="overflow-x-auto">
            <table className="w-full">
                <thead className="bg-gray-50">
                    <tr>
                        {columns.map(col => <th key={col} className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">{col.replace(/([A-Z])/g, ' $1')}</th>)}
                        <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Actions</th>
                    </tr>
                </thead>
                <tbody className="bg-white divide-y divide-gray-200">
                    {data.map(item => (
                        <tr key={item.bookId || item.publisherId || item.orderId} className="hover:bg-gray-50">
                            {columns.map(col => (
                                <td key={col} className="px-6 py-4 whitespace-nowrap text-gray-500">{item[col]}</td>
                            ))}
                            <td className="px-6 py-4 whitespace-nowrap text-sm">
                                <button onClick={() => onEdit(item)} className="text-indigo-600 hover:text-indigo-900 mr-3"><Edit2 size={18} /></button>
                                <button onClick={() => onDelete(item.bookId || item.publisherId || item.orderId)} className="text-red-600 hover:text-red-900"><Trash2 size={18} /></button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
}

//  Generic Modal 
function Modal({ type, mode, item, data, onClose, onSubmit }) {
    const [formData, setFormData] = useState(item || {});

    const handleChange = e => {
        const { name, value } = e.target;
        setFormData(prev => ({ ...prev, [name]: ['price', 'stockQuantity', 'publisherId', 'publicationYear', 'bookId', 'quantity'].includes(name) ? parseFloat(value) || 0 : value }));
    };

    const fields = formFields[type];
    return (
        <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50 p-4">
            <div className="bg-white rounded-lg shadow-xl max-w-2xl w-full max-h-[90vh] overflow-y-auto">
                <div className="px-6 py-4 border-b flex justify-between items-center">
                    <h2 className="text-xl font-bold text-gray-900">{mode === 'create' ? 'Add New' : 'Edit'} {type.slice(0, -1)}</h2>
                    <button onClick={onClose} className="text-gray-400 hover:text-gray-600"><svg className="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M6 18L18 6M6 6l12 12" /></svg></button>
                </div>
                <div className="p-6 space-y-4">
                    {fields.map(f => (
                        <div key={f.name}>
                            <label className="block text-sm font-medium text-gray-700 mb-1">{f.label}{f.required ? ' *' : ''}</label>
                            {f.type === 'select' ? (
                                <select name={f.name} value={formData[f.name] || ''} onChange={handleChange}
                                    className="w-full px-3 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500 focus:border-transparent">
                                    <option value="">Select {f.label}</option>
                                    {(f.options || data[f.optionsKey] || []).map(opt => typeof opt === 'object' ? <option key={opt.bookId || opt.publisherId} value={opt.bookId || opt.publisherId}>{opt.title || opt.name}</option> : <option key={opt} value={opt}>{opt}</option>)}
                                </select>
                            ) : (
                                <input type={f.type} name={f.name} value={formData[f.name] || ''} onChange={handleChange} className="w-full px-3 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-indigo-500 focus:border-transparent" />
                            )}
                        </div>
                    ))}
                    <div className="flex justify-end gap-3 pt-4 border-t">
                        <button onClick={onClose} className="px-6 py-2 border border-gray-300 rounded-lg text-gray-700 hover:bg-gray-50 font-medium">Cancel</button>
                        <button onClick={() => onSubmit(formData)} className="px-6 py-2 bg-indigo-600 text-white rounded-lg hover:bg-indigo-700 font-medium">{mode === 'create' ? 'Create' : 'Update'}</button>
                    </div>
                </div>
            </div>
        </div>
    );
}
