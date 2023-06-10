import React from 'react'
import TodoList from '../../components/Todos/TodoList'
import NavigationPanel from '../../components/Navigation/NavigationPanel'
import { useParams } from 'react-router-dom';
import { deleteTodo, editTodo, getAllTodos, getTodosWith } from '../../utils/services/TodoService';
import { updateDone } from '../../utils/services/TodoService'
import { useState, useEffect } from 'react';
import { ViewModelConfirm } from '../../utils/services/ViewService';

const HomePage = () => {

    const [todos, setTodos] = useState([]);
    const { completed } = useParams();


    useEffect(() => {

        async function fetchTodos() {
            if (!completed) {
                let responce = await getAllTodos();
                setTodos(responce);
            }
            else {
                setTodos(await getTodosWith(completed));
            }
        }
        fetchTodos();
    }, [])

    function handleDone(e, todo) {
        updateDone(todo.id, e.target.checked);
        const filteredTodos = todos.filter(td => td.id !== todo.id);

        todo.isDone = !todo.isDone;
        setTodos([todo, ...filteredTodos]);
    }
    function handleEditTitle(id, title) {
        var responce = editTodo(id, title);
    }
    function handleDelete(id) {
        if (ViewModelConfirm("Are you sure you want to delete the todo?")) {
            deleteTodo(id);
        }
    }

    return (
        <div className=''>
            {todos.length > 0 || todos ? (<>
                <TodoList todos={todos} handleDelete={handleDelete} handleDone={handleDone} handleEditTitle={handleEditTitle} />
            </>) : (<></>)}

        </div>
    )
}

export default HomePage