import axios from "axios";
import { API_URL } from "../Constants";

const token = localStorage.getItem('token');
const instance = axios.create({
    baseURL: `${API_URL}`,
    headers: { 'Authorization': `Bearer ${token}` }
});

export async function getAllTodos() {
    let responce;
    try {
        responce = await instance.get('todo')
        return responce.data;
    }
    catch (e) {
        if(e.status == 401)
            localStorage.removeItem('token');
        if(!responce.data)
            return [];
    }
}
export async function createTodo(title) {
    const token = localStorage.getItem('token');
    await fetch(`${API_URL}/todo/create`, {
        method: "POST",
        body: JSON.stringify({ title }),
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        }
    })
}
export async function updateDone(id, done) {
    const token = localStorage.getItem('token');
    await fetch(`${API_URL}/todo/done`, {
        method: "PUT",
        body: JSON.stringify({ id: id, setDone: done }),
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        }
    })
}
export async function getTodosWith(completed) {
    const token = localStorage.getItem('token');
    var responce = await fetch(`${API_URL}/todo/${completed}`, {
        method: "GET",
        headers: {
            'Authorization': `Bearer ${token}`
        }
    })
    return responce.json();
}
export async function editTodo(id, title) {
    const token = localStorage.getItem('token');
    var responce = await fetch(`${API_URL}/todo/update`, {
        method: "PUT",
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        },
        body: JSON.stringify({ id, newTitle: title })
    })
}

export async function deleteTodo(id) {
    const token = localStorage.getItem('token');
    var responce = await fetch(`${API_URL}/todo/delete`, {
        method: "DELETE",
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        },
        body: JSON.stringify({ id })
    })
}