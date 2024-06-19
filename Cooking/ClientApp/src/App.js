import { Provider } from 'react-redux';
import { Navigate, Route, BrowserRouter as Router, Routes } from 'react-router-dom';
import './App.css';
import AuthorizedLayout from './Components/Layouts/AuthorizedLayout';
import NonAuthorizedLayout from './Components/Layouts/NonAuthorizedLayout';
import Login from './Components/Login/Login';
import Register from './Components/Register/Register';
import store from './data/Store';
import ListRecipe from './Components/Recipe/ListRecipe/ListRecipe';
import CreateRecipe from './Components/Recipe/CreateRecipe/CreateRecipe';
import MyRecipes from './Components/Recipe/ListRecipe/MyRecipes/MyRecipes';
import GetRecipe from './Components/Recipe/GetRecipe/GetRecipe';

function App() {
  let isAuthorized = store.getState()?.AuthorizationReducer?.isAuthorized;
  return (
    <Provider store={store}>
      <div className="App">
        <Router>
          {
            isAuthorized ?
              <AuthorizedLayout logoutCallback={() => {isAuthorized = false}}>
                <Routes>
                  <Route path="/login" element={<Login />} />
                  <Route path="/register" element={<Register />} />
                  <Route path="/recipes" element={<ListRecipe />} />
                  <Route path="/recipes/create" element={<CreateRecipe />} />
                  <Route path="/recipes/owner" element={<MyRecipes />} />
                  <Route path="/recipes/:id" element={<GetRecipe />} />
                  <Route path="*" element={<ListRecipe />} />
                </Routes>
              </AuthorizedLayout>
              :
              <NonAuthorizedLayout>
                <Routes>
                  <Route path="/login" element={<Login />} />
                  <Route path="/register" element={<Register />} />
                  <Route path="/recipes" element={<ListRecipe />} />
                  <Route path="/recipes/create" element={<CreateRecipe />} />
                  <Route path="/recipes/owner" element={<MyRecipes />} />
                  <Route path="/recipes/:id" element={<GetRecipe />} />
                  <Route path="*" element={<ListRecipe />} />
                </Routes>
              </NonAuthorizedLayout>
          }
        </Router>
      </div>
    </Provider>
  );
}

export default App;
