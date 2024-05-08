import { Select } from 'antd';
import React, { useEffect, useState } from 'react';
import './Ingridient.css';
import bmw from './bmw.svg';



const Ingridient = ({products, ingridientId, updateIngridientInParentState, deleteIngridient}) => {
  
  const [name, setName] = useState('');
    

  const [calories, setCalories] = useState(0);
  const [proteins, setProteins] = useState(0);
  const [fats, setFats] = useState(0);
  const [carbohydrates, setCarbohydrates] = useState(0);

  const [newName, setNewName] = useState('');

  const [newCalories, setNewCalories] = useState(0);
  const [newProteins, setNewProteins] = useState(0);
  const [newFats, setNewFats] = useState(0);
  const [newCarbohydrates, setNewCarbohydrates] = useState(0);

  const [weight, setWeight] = useState(0);
  const [isNewProduct, setIsNewProduct] = useState(false);

    function checkboxChange(e){
        let ingridientNode = e.target.parentElement.parentElement;

        let checked = e.target.checked
        setIsNewProduct(checked)
        
        let newIngridient = {
          name:name,
        
          calories:calories,
          proteins:proteins,
          fats:fats,
          carbohydrates:carbohydrates,

          newName:newName,

          newCalories:newCalories,
          newProteins:newProteins,
          newFats:newFats,
          newCarbohydrates:newCarbohydrates,

          weight:weight,
          isNewProduct:checked,

          ingridientId:ingridientId,
        }
        updateIngridientInParentState(newIngridient)

        
        let selectName = ingridientNode.querySelector('.select-name')

        let divCalories = ingridientNode.querySelector('.div-calories')
        let divProteins = ingridientNode.querySelector('.div-proteins')
        let divFats = ingridientNode.querySelector('.div-fats')
        let divCarbohydrates = ingridientNode.querySelector('.div-carbohydrates')

        let inputName = ingridientNode.querySelector('.input-name')

        let inputCalories = ingridientNode.querySelector('.input-calories')
        let inputProteins = ingridientNode.querySelector('.input-proteins')
        let inputFats = ingridientNode.querySelector('.input-fats')
        let inputCarbohydrates = ingridientNode.querySelector('.input-carbohydrates')

        if(checked){

          selectName.style.display = 'none'

          divCalories.style.display = 'none'
          divProteins.style.display = 'none'
          divFats.style.display = 'none'
          divCarbohydrates.style.display = 'none'

          inputName.style.display = ''

          inputCalories.style.display = ''
          inputProteins.style.display = ''
          inputFats.style.display = ''
          inputCarbohydrates.style.display = ''
        }

        else{
          inputName.style.display = 'none'

          inputCalories.style.display = 'none'
          inputProteins.style.display = 'none'
          inputFats.style.display = 'none'
          inputCarbohydrates.style.display = 'none'

          selectName.style.display = ''

          divCalories.style.display = ''
          divProteins.style.display = ''
          divFats.style.display = ''
          divCarbohydrates.style.display = ''
            
        }
      }

      function selectChange(e){//создатели этой библиотеки для мазохистов решили поменять дефолтный объект события - это ужасное решение
        let selectedProduct = products.filter(x => x.value === e)[0]


        setName(e)
        setCalories(selectedProduct.calories)
        setProteins(selectedProduct.proteins)
        setFats(selectedProduct.fats)
        setCarbohydrates(selectedProduct.carbohydrates)
        
        let newIngridient = {
          name:e,
        
          calories:selectedProduct.calories,
          proteins:selectedProduct.proteins,
          fats:selectedProduct.fats,
          carbohydrates:selectedProduct.carbohydrates,

          newName:newName,

          newCalories:newCalories,
          newProteins:newProteins,
          newFats:newFats,
          newCarbohydrates:newCarbohydrates,

          weight:weight,
          isNewProduct:isNewProduct,

          ingridientId:ingridientId,
        }

        updateIngridientInParentState(newIngridient)
      }

    function inputNewCaloriesHandler(e){
      let localNewCalories = Number(e.target.value.replace(/\D/g,''))
      setNewCalories(localNewCalories)
      
      let newIngridient = {
        name:name,
        
        calories:calories,
        proteins:proteins,
        fats:fats,
        carbohydrates:carbohydrates,

        newName:newName,

        newCalories:localNewCalories,
        newProteins:newProteins,
        newFats:newFats,
        newCarbohydrates:newCarbohydrates,

        weight:weight,
        isNewProduct:isNewProduct,

        ingridientId:ingridientId,
      }

      updateIngridientInParentState(newIngridient)
    }

    function inputNewProteinsHandler(e){
      let localNewProteins = Number(e.target.value.replace(/\D/g,''))
      setNewProteins(localNewProteins)

      let localNewCalories = calculateCalories(localNewProteins, newFats, newCarbohydrates)
      setNewCalories(localNewCalories)

      let newIngridient = {
        name:name,
        
        calories:calories,
        proteins:proteins,
        fats:fats,
        carbohydrates:carbohydrates,

        newName:newName,

        newCalories:localNewCalories,
        newProteins:localNewProteins,
        newFats:newFats,
        newCarbohydrates:newCarbohydrates,

        weight:weight,
        isNewProduct:isNewProduct,

        ingridientId:ingridientId,
      }

      updateIngridientInParentState(newIngridient)
    }

    function inputNewFatsHandler(e){
      let localNewFats = Number(e.target.value.replace(/\D/g,''))
      setNewFats(localNewFats)

      let localNewCalories = calculateCalories(newProteins, localNewFats, newCarbohydrates)
      setNewCalories(localNewCalories)
      
      let newIngridient = {
        name:name,
        
        calories:calories,
        proteins:proteins,
        fats:fats,
        carbohydrates:carbohydrates,

        newName:newName,

        newCalories:localNewCalories,
        newProteins:newProteins,
        newFats:localNewFats,
        newCarbohydrates:newCarbohydrates,

        weight:weight,
        isNewProduct:isNewProduct,

        ingridientId:ingridientId,
      }

      updateIngridientInParentState(newIngridient)
    }

    function inputNewCarbohydratesHandler(e){
      let localNewCarbohydrates = Number(e.target.value.replace(/\D/g,''))
      setNewCarbohydrates(localNewCarbohydrates)
      let localNewCalories = calculateCalories(newProteins, newFats, localNewCarbohydrates)
      setNewCalories(localNewCalories)
      
      let newIngridient = {
        name:name,
        
        calories:calories,
        proteins:proteins,
        fats:fats,
        carbohydrates:carbohydrates,

        newName:newName,

        newCalories:localNewCalories,
        newProteins:newProteins,
        newFats:newFats,
        newCarbohydrates:localNewCarbohydrates,

        weight:weight,
        isNewProduct:isNewProduct,

        ingridientId:ingridientId,
      }

      updateIngridientInParentState(newIngridient)
    }

    function inputWeightHandler(e){
      let localWeight = Number(e.target.value.replace(/\D/g,'')) 
      setWeight(localWeight)
      
      let newIngridient = {
        name:name,
        
        calories:calories,
        proteins:proteins,
        fats:fats,
        carbohydrates:carbohydrates,

        newName:newName,

        newCalories:newCalories,
        newProteins:newProteins,
        newFats:newFats,
        newCarbohydrates:newCarbohydrates,

        weight:localWeight,
        isNewProduct:isNewProduct,

        ingridientId:ingridientId,
      }

      updateIngridientInParentState(newIngridient)
    }
    
    function inputNameHandler(e){
      setNewName(e.target.value)
      
      let newIngridient = {
        name:name,
        
        calories:calories,
        proteins:proteins,
        fats:fats,
        carbohydrates:carbohydrates,

        newName:e.target.value,

        newCalories:newCalories,
        newProteins:newProteins,
        newFats:newFats,
        newCarbohydrates:newCarbohydrates,

        weight:weight,
        isNewProduct:isNewProduct,

        ingridientId:ingridientId,
      }

      updateIngridientInParentState(newIngridient)
    }

    function calculateCalories(proteins, fats, carbohydrates){
      return proteins * 4 + fats * 9 + carbohydrates * 4
    }

    return (
        <tr className="ingridient" id={ingridientId}>
            <td className="td-name">
              <input type="text" className='input-name textbox-short-number' value={newName} onInput={event => inputNameHandler(event)} style={{display:'none'}}></input>
              <Select showSearch={true} optionFilterProp="label" options={products} onChange={event => selectChange(event)} value={name} className='select-name'/>
              </td>
            <td className="td-calories">
              <div type="text" className="input-calories div-short-number" onInput={event => inputNewCaloriesHandler(event)} style={{display:'none'}}>{newCalories}</div>
              <div className="div-short-number div-calories">{calories}</div>
              </td>
            <td className="td-proteins">
              <input type="text" className="input-proteins textbox-short-number" value={newProteins} onInput={event => inputNewProteinsHandler(event)} style={{display:'none'}}></input>
              <div className="div-short-number div-proteins">{proteins}</div>
              </td>
            <td className="td-fats">
              <input type="text" className="input-fats textbox-short-number" value={newFats} onInput={event => inputNewFatsHandler(event)}  style={{display:'none'}}></input>
              <div className="div-short-number div-fats">{fats}</div>
              </td>
            <td className="td-carbohydrates">
              <input type="text" className="input-carbohydrates textbox-short-number" value={newCarbohydrates} onInput={event => inputNewCarbohydratesHandler(event)}  style={{display:'none'}}></input>
              <div className="div-short-number div-carbohydrates">{carbohydrates}</div>
              </td>
            <td className="td-weight">
              <input type="text" className="weight textbox-short-number" value={weight} onInput={event => inputWeightHandler(event)}></input>
              </td>
            <td><input type="checkbox" value={isNewProduct} onChange={checkboxChange}/></td>
            <td><img src={bmw} onClick={() => deleteIngridient(ingridientId)} style={{cursor:'pointer'}}></img></td>
        </tr>
        );
}

export default Ingridient;