import { SET_CURRENT_USER } from '../actionCreators/Types.js'
import isEmpty from 'lodash/isEmpty';
const InitialState = {
    isAuthorized: false,
    isModerator: false,
    user: {}
}
export default (state = InitialState, action = {}) => {
    switch (action.type) {
        case SET_CURRENT_USER:
            return {
                isAuthorized: !isEmpty(action.user),
                isModerator: action.isModerator,
                user: action.user,
            };
        default:
            return state;
    }
}