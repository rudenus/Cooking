import { Provider } from 'react-redux';
import { Navigate, Route, BrowserRouter as Router, Routes } from 'react-router-dom';
import './App.css';
import AuthorizedLayout from './Components/Layouts/AuthorizedLayout';
import NonAuthorizedLayout from './Components/Layouts/NonAuthorizedLayout';
import Login from './Components/Login/Login';
import MainPage from './Components/MainPage/MainPage';
import Register from './Components/Register/Register';
import store from './data/Store';

function App() {
  const isAuthorized = store.getState()?.AuthorizationReducer?.isAuthorized;
  return (
    <Provider store={store}>
      <div className="App">
        <Router>
          {
            isAuthorized ?
              <AuthorizedLayout>
                <Routes>
                  <Route path="/login" element={<Navigate to="/" replace />} />
                  <Route path="*" element={<MainPage />} />
                </Routes>
              </AuthorizedLayout>
              :
              <NonAuthorizedLayout>
                <Routes>
                  <Route path="/login" element={<Login />} />
                  <Route path="/register" element={<Register />} />
                  <Route path="*" element={<MainPage />} />
                </Routes>
              </NonAuthorizedLayout>
          }
        </Router>
      </div>
    </Provider>
  );
}

export default App;
