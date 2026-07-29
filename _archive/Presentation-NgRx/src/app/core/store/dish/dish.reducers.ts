import { DishActions, EDishActions } from './dish.actions';
import { IDishState, initialDishState } from './dish.state';

export const dishReducers = (
  state = initialDishState,
  action: DishActions
): IDishState => {
  switch (action.type) {
    case EDishActions.GetDishesSuccess: {
      return {
        ...state,
        dishes: action.payload,
      };
    }
    case EDishActions.GetDishSuccess: {
      return {
        ...state,
        selectedDish: action.payload,
      };
    }
    default:
      return state;
  }
};

// export const initialState = [];

// export function DishReducer(state = initialState, action: ActionEx) {
//   switch (action.type) {
//     case DishActions.Add:
//       return [...state, action.payload];
//     case DishActions.Remove:
//       return [
//         ...state.slice(0, action.payload),
//         ...state.slice(action.payload + 1),
//       ];
//     default:
//       return state;
//   }
// }
