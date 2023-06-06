import React, { useEffect, useState } from 'react'
import { Navigate, Outlet } from 'react-router-dom';

const PrivateRouter = () => {
    const [isAuth, setAuthorize] = useState( false);
    const [isLoading, setIsLoading] = useState(true);

    useEffect(() => {
      async function setAuth() {
        const token = localStorage.getItem('token');
          await setAuthorize(!!token)
          await setIsLoading(false);
        }
        setAuth();
      }, [])
    
      if (isLoading) {
        return <div>Loading...</div>;
      }
      else {
        return (
            <>
            {isAuth ? <Outlet /> : <Navigate to="/account" />}
            </>
        )
      }
}

export default PrivateRouter