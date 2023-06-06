import React, { useState } from 'react'
import TodoDate from './TodoDate'

const TodoItem = ({ todo, handleDone, handleEditTitle, handleDelete }) => {

    const [isEdit, setEdit] = useState(false)
    const [newTitle, setNewTitle] = useState(todo.title);

    return (
        <div className='col-4 my-2'>
            <div className='card '>
                <div className="card-body">
                    <div className='row'>
                        <div className="col-4">
                            <input type="checkbox" onChange={(event) => { handleDone(event, todo) }} checked={todo.isDone} />
                            <h5>Is done</h5>
                        </div>
                        <div className=' col-8 mb-3'>
                            <div className='input-group'>
                                <input type='text' disabled={!isEdit} className='col-6' onChange={(e) => setNewTitle(e.target.value)} value={newTitle} />
                                {isEdit ? (<>
                                    <button className=' btn btn-success btn-sm' onClick={() => { handleEditTitle(todo.id, newTitle); setEdit(false) }}>Submit</button>
                                </>) : (<></>)
                                }
                            </div>
                        </div>
                    </div>
                    <div className='row'>
                        <a onClick={()=>{ handleDelete(todo.id)}} className="btn btn-danger col-5 m-2">Delete</a>
                        <a onClick={() => { setEdit(e => !e) }} className="btn btn-info col-5 m-2">Edit title</a>
                    </div>
                </div>
                <div className="card-footer">
                    <TodoDate date={todo.date} />
                </div>
            </div>
        </div>
    )
}

export default TodoItem