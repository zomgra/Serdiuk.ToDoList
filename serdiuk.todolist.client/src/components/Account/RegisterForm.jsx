import React from 'react'
import { register } from '../../utils/services/AuthService'

const RegisterForm = () => {
  return (
    <div >
        <input name='email' className='form-control' placeholder='Email'/>
        <input name='password' className='form-control' placeholder='Password'/>
        <input name='confirmPassword' className='form-control' placeholder='Confirm password'/>
     </div>
  )
}

export default RegisterForm