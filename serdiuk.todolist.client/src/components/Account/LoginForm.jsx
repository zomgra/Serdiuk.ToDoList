import React from 'react'
import { login } from '../../utils/services/AuthService'

const LoginForm = () => {
    return (
        <div>
            <input name='email' className='form-control' placeholder='Email'/>
        <input name='password' className='form-control' placeholder='Password'/>
        </div>
    )
}

export default LoginForm