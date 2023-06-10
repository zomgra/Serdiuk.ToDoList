import React from 'react'
import { register } from '../../utils/services/AuthService'

const RegisterForm = () => {
  return (
    <div className='row col'>
        <input name='email' className='form-control m-1' placeholder='Email'/>
        <input name='password' className='form-control m-1' placeholder='Password'/>
        <input name='confirmPassword' className='form-control m-1' placeholder='Confirm password'/>
     </div>
  )
}

export default RegisterForm