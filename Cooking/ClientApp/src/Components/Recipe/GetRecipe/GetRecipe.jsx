import React, { useEffect, useState } from 'react';
import api from "../../../api/api";
import { Editor } from 'primereact/editor';
import { Button } from 'antd';
import { useNavigate, useParams } from 'react-router-dom';
import { render } from 'react-dom';
import store from '../../../data/Store';


const GetRecipe = () => {
  let navigate = useNavigate();
    const { id } = useParams();
    const isModerator = store.getState()?.AuthorizationReducer?.user?.isModerator;

    const [isModerated, setIsModerated] = useState(true);
    const [showApproveButton, setshowApproveButton] = useState(false);

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
    let rendered = false;

    

    useEffect(() => {
        api.get(`/Recipe/${id}`).then(res => {
            if(rendered === true){
                return;
            }
            setIsModerated(res.data.isModerated)
            
            setDescription(res.data.description);
            setName(res.data.name);
            setWeightSum(res.data.weight)
            setCaloriesSum(res.data.calories)
            setCarbohydratesSum(res.data.carbohydrates)
            setFatsSum(res.data.fats)
            setProteinsSum(res.data.proteins)

            setIngridientNodes([]);
            res.data.ingridients.forEach(element => {
                addIngridient(element);
            });

            setStepNodes([]);
            res.data.operations.forEach(element => {
                addStep(element)
            });
            console.log(res.data.isModerated);
            console.log(isModerator)
            setshowApproveButton(isModerator && !res.data.isModerated)
            rendered = true;
        });
    }, []);

    function addIngridient(ingridient){
        let newNode = 
        <tr className="ingridient">
            <td className="td-name">
            <div type="text" className='input-name div-short-number'>{ingridient.name}</div>
              </td>
            <td className="td-calories">
              <div className="div-short-number div-calories">{ingridient.calories}</div>
              </td>
            <td className="td-proteins">
              <div className="div-short-number div-proteins">{ingridient.proteins}</div>
              </td>
            <td className="td-fats">
              <div className="div-short-number div-fats">{ingridient.fats}</div>
              </td>
            <td className="td-carbohydrates">
              <div className="div-short-number div-carbohydrates">{ingridient.carbohydrates}</div>
              </td>
            <td className="td-weight">
              <div className="weight div-short-number">{ingridient.weight}</div>
            </td>
        </tr>
        setIngridientNodes(result => [...result, newNode])
    }

    function addStep(step){
        let src =  "data:image/png;base64," + step.file;
        let newNode =
        <div className="step">
            <div>
                <table style={{margin:'0 auto'}}>
                    <tbody>
                        <tr>
                            <td>
                                <Editor value={step.description} textAlign='center' showHeader={false} readOnly={true} style={{ height: '350px', width: '700px', border:'0px', textAlign:'center'}} className='step-description'/>
                            </td>
                            <td valign='top' textAlign='center' style={{valign:'top', textAlign:'center', minWidth:'600px', marginTop:'40px'}}>
                                <img alt="preview image" src={src} style={{textAlign:'left', maxWidth:'700px', maxHeight:'450px', margin:'30px auto 0 auto'}} className='preview-image'/>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
        setStepNodes(result => [...result, newNode])
    }

    return (
        <div className="app">
          <div className='input-recipe-name'>
          <h2 type='text' id='input-name' style={{margin:'30px auto 40px auto', textAlign:'center' }}>{name}</h2>
          </div>

          {showApproveButton && <Button onClick={(e)=>{
            api.post(`/moderator/approve-recipe/${id}`)
          }}  type="primary" >Одобрить</Button>}

          <Editor value={description} showHeader={false} className='input-description' readOnly style={{ minHeight: '220px' }}/>
          
          <table id="list-ingridients"  style={{minHeight:'100px', fontSize:'18px'}}>
            <tbody >
              <tr>
                <td className="td-ingridient"><div>Название</div></td>
                <td className="td-ingridient"><div>Каллории на 100г.</div></td>
                <td className="td-ingridient"><div>Белки на 100г.</div></td>
                <td className="td-ingridient"><div>Жиры на 100г.</div></td>
                <td className="td-ingridient"><div>Углеводы на 100г.</div></td>
                <td className="td-ingridient"><div>Вес</div></td>
              </tr>
              {ingridientNodes}
              <tr className='result-sums'>
                <td className="td-ingridient"><div style={{fontWeight:600}}>Итого</div></td>
                <td className="td-ingridient"><div>{caloriesSum}</div></td>
                <td className="td-ingridient"><div>{proteinsSum}</div></td>
                <td className="td-ingridient"><div>{fatsSum}</div></td>
                <td className="td-ingridient"><div>{carbohydratesSum}</div></td>
                <td className="td-ingridient"><div type="text" className='div-short-number'>{weightSum}</div></td>
              </tr>
            </tbody>
          </table>
            <div id="list-steps">
              {stepNodes}
            </div>
        </div>
        );
}

export default GetRecipe;