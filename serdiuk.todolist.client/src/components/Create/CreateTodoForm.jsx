import React from 'react'

const CreateTodoForm = ({ createTodoHandler }) => {
  return (
    <div className='input-group' style={{maxWidth:'998px'}}>
      <form onSubmit={(e) => createTodoHandler(e)} className='flex-nowrap input-group'>
        <input name='title' className='form-control' placeholder='Input title for todo' />
        <button className='btn btn-info ' type='submit'>Create</button>
      </form>
    </div>
  )
}

export default CreateTodoForm