import React, { useState } from 'react'
import LoginForm from '../../components/Account/LoginForm';
import RegisterForm from '../../components/Account/RegisterForm';
import { login, register } from '../../utils/services/AuthService';

const AccountPage = () => {
    const [isLoginPage, setLoginPage] = useState(true);
    async function handleForm(e) {
        e.preventDefault();
        isLoginPage ? await login(e.target.email.value, e.target.password.value) : await register(e.target.email.value, e.target.password.value, e.target.confirmPassword.value)
    }

    return (
        <div className='signin-form'>
            <form className='input-group' onSubmit={(e) => handleForm(e)}>
                {isLoginPage ? (
                    <div className='row'>
                        <LoginForm />
                        <div className='row mx-3 col-3'>
                            <a className='btn btn-primary mb-1' onClick={(e) => { setLoginPage(false) }}>Create account</a>
                            <button type='submit' className='btn btn-info'>Log in</button>
                        </div>
                    </div>
                ) : (
                    <div className='row'>
                        <RegisterForm />
                        <div className='row mx-3 col-3'>
                            <a className='btn my-3 btn-primary' onClick={(e) => { setLoginPage(true) }}>Already have account</a>
                            <button type='submit' className='btn btn-info'>Create account</button>
                        </div>
                    </div>
                )}
            </form>
        </div>
    )
}

export default AccountPage