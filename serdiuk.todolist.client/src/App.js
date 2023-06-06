import { Route, Routes } from 'react-router-dom';
import './App.css';
import TodoList from './components/Todos/TodoList';
import PrivateRouter from './utils/route/PrivateRoute';
import AccountPage from './Pages/Account/AccountPage';
import HomePage from './Pages/Home/HomePage';
import NavigationPanel from './components/Navigation/NavigationPanel';
import CreatePage from './Pages/Create/CreatePage';

function App() {
  return (
    <div className='container-fluid'>
      <NavigationPanel/>
      <Routes >
        <Route element={<PrivateRouter />}>
          <Route path='/:completed' element={<HomePage />}></Route>
          <Route path='/' element={<HomePage />}></Route>
          <Route path='/create' element={<CreatePage/>}></Route>
        </Route>
        <Route element={<AccountPage />} path='/account'></Route>
      </Routes>
    </div>
  );
}

export default App;
