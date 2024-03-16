import thunk from 'redux-thunk';
import { createStore, applyMiddleware, compose, combineReducers } from 'redux';
import AuthorizationReducer from './reducers/AuthorizationReducer';

const RootReducer = combineReducers({ AuthorizationReducer });

const store = createStore(RootReducer, compose(applyMiddleware(thunk)));

export default store;