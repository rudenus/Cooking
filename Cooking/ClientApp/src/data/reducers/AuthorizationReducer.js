import { SET_CURRENT_USER } from '../actionCreators/Types.js'
import isEmpty from 'lodash/isEmpty';
const InitialState = {
    isAuthorized: false,
    isAdmin: false,
    user: {}
}
export default (state = InitialState, action = {}) => {
    switch (action.type) {
        case SET_CURRENT_USER:
            return {
                isAuthorized: !isEmpty(action.user),
                isAdmin: action.user?.role === "Administrator",
                user: action.user,
            };
        default:
            return state;
    }
}