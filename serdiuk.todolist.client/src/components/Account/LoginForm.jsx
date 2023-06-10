import React from 'react'
import { login } from '../../utils/services/AuthService'

const LoginForm = () => {
    return (
        <div className='row col'>
            <input name='email' className='form-control col-9 mb-1' placeholder='Email'/>
            <input name='password' className='form-control col-3 mt-1' placeholder='Password'/>    
        </div>
    )
}

export default LoginForm