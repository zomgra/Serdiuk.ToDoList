import React from 'react'
import TodoItem from './TodoItem'
const TodoList = ({todos, handleDone, handleEditTitle, handleDelete}) => {

    if(!todos) return(
        <div>
            Empty
        </div>
    )
    return (
        <div className='row'>
            {todos.map((todo) => (
                <TodoItem key={todo.id} todo={todo} handleDelete={handleDelete} handleDone={handleDone} handleEditTitle={handleEditTitle}/>
            ))}
        </div>
    )
}

export default TodoList