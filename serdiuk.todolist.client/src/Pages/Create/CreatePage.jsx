import React from 'react'
import { createTodo } from '../../utils/services/TodoService'
import { useNavigate } from 'react-router-dom';
import CreateTodoForm from '../../components/Create/CreateTodoForm';

const CreatePage = () => {

    const navigate = useNavigate();

 async function createTodoHandler(e) {
    e.preventDefault();
    var responce = await createTodo(e.target.title.value)
    .then(res=> navigate("/"))
    .catch(error => console.error(error));
 }

  return (
    <div>
        <CreateTodoForm createTodoHandler={createTodoHandler}/>
    </div>
  )
}

export default CreatePage