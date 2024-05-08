import React, { useCallback, useEffect, useState } from 'react';
import './CreateRecipe.css';
import api from "../../../api/api";
import Ingridient from './Ingridient';
import Step from './Step';
import { Editor } from 'primereact/editor';
import plus from './plus.png';
import { Button } from 'antd';
import { useNavigate } from 'react-router-dom';


const CreateRecipe = () => {
  let navigate = useNavigate();
  
  const header = (
    <div>
        <span className="ql-formats">
            <button className="ql-bold" aria-label="Bold"></button>
            <button className="ql-italic" aria-label="Italic"></button>
            <button className="ql-underline" aria-label="Underline"></button>
        </span>
    </div>
    );

    const [weightSum, setWeightSum] = useState(0);
    const [caloriesSum, setCaloriesSum] = useState(0);
    const [proteinsSum, setProteinsSum] = useState(0);
    const [fatsSum, setFatsSum] = useState(0);
    const [carbohydratesSum, setCarbohydratesSum] = useState(0);

    const [name, setName] = useState('');
    const [description, setDescription] = useState('');

    const [ingridients, setIngridients] = useState([]);

    const [products, setProducts] = useState('');

    const [ingridientCount, setIngridientsCount] = useState(0);
    const [ingridientNodes, setIngridientNodes] = useState([]);
    
    const [steps, setSteps] = useState([]);
    const [stepsCount, setStepsCount] = useState(1);
    const [stepNodes, setStepNodes] = useState([]);
    useEffect(() => {
        api.get("/Product").then(res => {
            let list = res.data.map(x => ({
              value:x.productId,
              label:x.name,
              calories:x.calories,
              fats:x.fats,
              proteins:x.proteins,
              carbohydrates:x.carbohydrates}));//говнокод, но времени мало 
            setProducts(list);
        });
    }, []);

    const deleteIngridient = (key) =>{
      setIngridientNodes(actualIngridientNodes => {
        return actualIngridientNodes.filter(node => node.key !== key)
      })

      setIngridients(actualIngridients => {
        let newIngridients = actualIngridients.filter(node => node.ingridientId !== key)
        updateAllSums(newIngridients);
        return newIngridients
      })
    };

    function addIngridientClick(e){
        setIngridientsCount(ingridientCount+1);
        let ingridientId = "ingridient" + ingridientCount;
        let newNode = <Ingridient products={products} ingridientId={ingridientId} updateIngridientInParentState={updateIngridient} deleteIngridient={deleteIngridient} key={ingridientId}></Ingridient>
        setIngridientNodes(result => [...result, newNode])
    }

    function updateIngridient(newIngridient){
      setIngridients(actualingridientNodes => {
        console.log(actualingridientNodes)
        if(actualingridientNodes.filter(ingridient => ingridient.ingridientId == newIngridient.ingridientId).length === 0){
          let newIngridients = [...actualingridientNodes, newIngridient]
          updateAllSums(newIngridients);
          return newIngridients;
        }
  
        let newIngridients = actualingridientNodes.map(ingridient =>{
          if(ingridient.ingridientId == newIngridient.ingridientId){
            return newIngridient
          }
          return ingridient
        })

        updateAllSums(newIngridients);

        return newIngridients;
      })
    }

    function addStepClick(e){
      setStepsCount(stepsCount+1);
      let stepId = "step" + stepsCount;
      let newNode = <Step steps={steps} stepId={stepId} updateStepInParentState={updateStep} key={stepId}></Step>
      setStepNodes(result => [...result, newNode])
  }

  function updateStep(newStep){
    setSteps(actualStepsNodes => {
      console.log(actualStepsNodes)
      if(actualStepsNodes.filter(step => step.stepId == newStep.stepId).length === 0){
        let newSteps = [...actualStepsNodes, newStep]
        return newSteps
      }

      let newSteps = actualStepsNodes.map(step =>{
        if(step.stepId == newStep.stepId){
          return newStep
        }
        return step
      })

      return newSteps
    })
  }

  function createRecipe(e){
    

    var formData = getFormData();


    api.post("/Recipe", formData).then(res => {
      navigate("/recipes")
  });
  }

  //https://stackoverflow.com/questions/67231362/why-iformfile-is-null-in-nested-object
  //из-за этого автоматическая серилизаций не подоходит
  function getFormData(){
    function getStepFromStepId(stepId){
      return stepId.replace("step", "")
    }

    var formData = new FormData();
    formData.append(`description`, description);
    formData.append(`name`, name);
    formData.append(`weight`, weightSum);

    for(let i = 0; i < ingridients.length; i++){
      formData.append(`ingridients[${i}][weight]`, ingridients[i].weight);
      
      if(ingridients[i].isNewProduct==false){
        formData.append(`ingridients[${i}][existingProductId]`, ingridients[i].name)
      }

      else{
        formData.append(`ingridients[${i}][newProduct][name]`, ingridients[i].newName)
        formData.append(`ingridients[${i}][newProduct][calories]`, ingridients[i].newCalories)
        formData.append(`ingridients[${i}][newProduct][proteins]`, ingridients[i].newProteins)
        formData.append(`ingridients[${i}][newProduct][fats]`, ingridients[i].newFats)
        formData.append(`ingridients[${i}][newProduct][carbohydrates]`, ingridients[i].newCarbohydrates)
      }
    }

    
    for(let i = 0; i < steps.length; i++){
      formData.append(`Operations[${i}].step`, getStepFromStepId(steps[i].stepId));
      formData.append(`Operations[${i}].timeInSeconds`, steps[i].timeInSeconds);
      formData.append(`Operations[${i}].description`, steps[i].description);
      formData.append(`Operations[${i}].File`, steps[i].file);
    }

    return formData
  }

  function descriptionChangeHandler(value){
    setDescription(value)
  }

  function nameChangeHandler(e){
    setName(e.target.value)
  }

  function weightSumHandler(e){
    setWeightSum(Number(e.target.value.replace(/\D/g,'')))
  }

  function updateAllSums(ingridients){

    let weightFromIngridients = ingridients.reduce((sum, current) =>{
      return sum + current.weight
    }, 0)
    setWeightSum(weightFromIngridients)

    if(weightFromIngridients === 0){
      setCaloriesSum(0)
      setProteinsSum(0)
      setFatsSum(0)
      setCarbohydratesSum(0)
      return
    }

    let caloriesFromIngridients = ingridients.reduce((sum, current) =>{
      if(current.isNewProduct){
        return sum + Math.round(current.newCalories / 100 * current.weight)
      }
      else{
        return sum + Math.round(current.calories / 100 * current.weight)
      }
    }, 0)
    let caloriesPer100 = Math.round(caloriesFromIngridients * 100 / weightFromIngridients)
    setCaloriesSum(caloriesPer100)

    let proteinsFromIngridients = ingridients.reduce((sum, current) =>{
      if(current.isNewProduct){
        return sum + Math.round(current.newProteins / 100 * current.weight)
      }
      else{
        return sum + Math.round(current.proteins / 100 * current.weight)
      }
    }, 0)
    let proteinsPer100 = Math.round(proteinsFromIngridients * 100 / weightFromIngridients)
    setProteinsSum(proteinsPer100)

    let fatsFromIngridients = ingridients.reduce((sum, current) =>{
      if(current.isNewProduct){
        return sum + Math.round(current.newFats / 100 * current.weight)
      }
      else{
        return sum + Math.round(current.fats / 100 * current.weight)
      }
    }, 0)
    let fatsPer100 = Math.round(fatsFromIngridients * 100 / weightFromIngridients)
    setFatsSum(fatsPer100)

    let carbohydratesFromIngridients = ingridients.reduce((sum, current) =>{
      if(current.isNewProduct){
        return sum + Math.round(current.newCarbohydrates / 100 * current.weight)
      }
      else{
        return sum + Math.round(current.carbohydrates / 100 * current.weight)
      }
    }, 0)
    let carbohydratesPer100 = Math.round(carbohydratesFromIngridients * 100 / weightFromIngridients)
    setCarbohydratesSum(carbohydratesPer100)

  }

    return (
        <div className="app">
          <div className='input-recipe-name'>
            <span style={{margin:'30px'}}>Введите название рецепта: </span><input type='text' value={name} id='input-name' onInput={nameChangeHandler} style={{padding:'3px 10px', minWidth:'300px'}}></input>
          </div>
          <div style={{fontSize:'18px', textAlign:'left', margin:'40px 10px 10px 130px'}}>Введите текстовое описание рецепта:</div>
          <Editor value={description}  onTextChange={(e) => descriptionChangeHandler(e.htmlValue)} headerTemplate={header} className='input-description'  style={{ minHeight: '220px' }}/>
          
          <div style={{margin:'30px', fontSize:'18px'}}>Укажите все необходимые ингридиенты</div>
          <table id="list-ingridients"  style={{minHeight:'100px', fontSize:'18px'}}>
            <tbody >
              <tr>
                <td className="td-ingridient"><div>Название</div></td>
                <td className="td-ingridient"><div>Каллории на 100г.</div></td>
                <td className="td-ingridient"><div>Белки на 100г.</div></td>
                <td className="td-ingridient"><div>Жиры на 100г.</div></td>
                <td className="td-ingridient"><div>Углеводы на 100г.</div></td>
                <td className="td-ingridient"><div>Вес</div></td>
                <td className="td-ingridient"><div>Новый продукт</div></td>
                <td className="td-ingridient"><div>Удалить</div></td>
              </tr>
              {ingridientNodes}
              <tr className='result-sums'>
                <td className="td-ingridient"><div style={{fontWeight:600}}>Итого</div></td>
                <td className="td-ingridient"><div>{caloriesSum}</div></td>
                <td className="td-ingridient"><div>{proteinsSum}</div></td>
                <td className="td-ingridient"><div>{fatsSum}</div></td>
                <td className="td-ingridient"><div>{carbohydratesSum}</div></td>
                <td className="td-ingridient"><input type="text" value={weightSum} onInput={weightSumHandler} className='textbox-short-number' /></td>
              </tr>
            </tbody>
          </table>
          <div style={{textAlign:'left', margin:'0px 10px 10px 200px'}}><img src={plus} alt="new-ingridient" onClick={addIngridientClick}  className='add-ingridient-img'  style={{cursor:'pointer'}}/></div>
          <div style={{fontSize:'18px', textAlign:'left', margin:'40px 10px 10px 130px'}}>Укажите шаги, необходимые для повторения рецепта</div>
            <div id="list-steps">
              {stepNodes}
            </div>
          <div>
            <Button id="button-add-new-step" type="primary" onClick={addStepClick} style={{textAlign:'left', margin:'40px 10px 10px 130px', display:'inline'}}>Добавить шаг</Button>
          </div>
          <Button type='primary' id="button-create-recipe" onClick={createRecipe}>Создать рецепт</Button>
        </div>
        );
}

export default CreateRecipe;