import { Button, Input, Table, Select, Radio, Slider } from 'antd';
import React, { useEffect, useState } from 'react';
import api from "../../../api/api";
import './ListRecipe.css';
import store from '../../../data/Store';
import { useNavigate } from 'react-router-dom';

const ListRecipe = (params) => {
    const isModerator = store.getState()?.AuthorizationReducer?.user?.isModerator;


    const [recipes, setArray] = useState([]);

    const [productsFilter, setProductsFilter] = useState([]);

    const [products, setProducts] = useState('');

    let navigate = useNavigate();

    const [name, setName] = useState('');

    const [showAdditionalFilters, setShowAdditionalFilters] = useState(false);

    const [caloriesMin, setCaloriesMin] = useState('');
    const [caloriesMax, setCaloriesMax] = useState('');
    const [proteinsMin, setProteinsMin] = useState('');
    const [proteinsMax, setProteinsMax] = useState('');
    const [fatsMin, setFatsMin] = useState('');
    const [fatsMax, setFatsMax] = useState('');
    const [carbohydratesMin, setCarbohydratesMin] = useState('');
    const [carbohydratesMax, setCarbohydratesMax] = useState('');

    const [radioGroupValue, setRadioGroupValue] = useState(1);
    const [replacementLevel, setReplacementLevel] = useState(null);
    const [replacementLevelSpan, setReplacementLevelSpan] = useState("Не учитывать");

    const [replacementLevelText, setReplacementLevelText] = useState(null);


    const columns = [
        {
            title: 'isTest',
            dataIndex: 'isTest',
            hide: true,
            key: 'isTest',
        },
        {
            title: 'Идентификатор рецепта',
            dataIndex: 'recipeId',
            hide: true,
            key: 'recipeId',
        },
        {
            title: 'Название',
            dataIndex: 'name',
            key: 'name',
        },
        {
            title: 'Каллории на 100г.',
            dataIndex: 'caloriesPer100',
            key: 'caloriesPer100',
        },
        {
            title: 'Белки на 100г.',
            dataIndex: 'proteinsPer100',
            key: 'proteinsPer100',
        },
        {
            title: 'Жиры на 100г.',
            dataIndex: 'fatsPer100',
            key: 'fatsPer100',
        },
        {
            title: 'Углеводы на 100г.',
            dataIndex: 'carbohydratesPer100',
            key: 'carbohydratesPer100',
        },
        {
            title: 'Используемые продукты',
            dataIndex: 'products',
            key: 'products',
        }
        ];

    useEffect(() => {
        api.get("/Recipe", {params : {
            PageNumber : 10,
            OnlyTheirOwn:params.onlyTheirOwn,
        }}).then(res => {
            setArray(res.data);
            console.log(res.data);
        });

        let url = "list"
        if(isModerator == true){
            url = "list-moderator"
        }

        api.get(`/Product/${url}`).then(res => {
            let list = res.data.map(x => ({
              value:x.productId,
              label:x.name}));
            setProducts(list);
        });

    }, []);

    function onColumn(e){
        console.log(e)
    }

    function onSliderChange(e){
        let text = "Не учитывать"
        let english = null
        if(e === 0){
            text = "Не учитывать"
            english = null
        }
        else if(e === 25){
            text = "Слабо"
            english = "Low"
        }
        else if(e === 50){
            text = "Средне"
            english = "Medium"
        }
        else if(e === 75){
            text = "Сильно"
            english = "Hard"
        }
        setReplacementLevel(english)
        setReplacementLevelSpan(text)
    }

    function onPaginationClick(e){
        console.log(e)
    }

    return (
        <div>
        <table style={{ width: 1200, margin: "20px auto" }}>
            <tbody>
                <tr>
                    <td className="column-filter" style={{maxWidth:'200px'}}>
                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <label>Название</label>
                                    </td>
                                    <td  style={{marginRight:'40px'}}>
                                        <Input 
                                            style={{marginRight:'50px'}}
                                            value={name}
                                            placeholder='Поиск'
                                            className="filter-input-small-text"
                                            type="text"
                                            name = "caloriesMin"
                                            onChange={(e) => { setName(e.target.value) }} />
                                    </td>
                                    <td  >
                                        <Button style={{marginRight:'20px', marginLeft:'20px', padding:'20px', paddingBottom:'40px' }} type="primary"
                                            verticalAlign={'center'}
                                            onClick={() => {
                                                let queryParams = {
                                                    Name : name,
                                                }
                                                if(showAdditionalFilters){
                                                    queryParams.CaloriesMin = caloriesMin
                                                    queryParams.CaloriesMax = caloriesMax
                                                    queryParams.ProteinsMin = proteinsMin
                                                    queryParams.ProteinsMax = proteinsMax
                                                    queryParams.OnlyTheirOwn = params.onlyTheirOwn
                                                    queryParams.FatsMin = fatsMin
                                                    queryParams.FatsMax = fatsMax
                                                    queryParams.CarbohydratesMin = carbohydratesMin
                                                    queryParams.CarbohydratesMax = carbohydratesMax
                                                    queryParams.Products = productsFilter
                                                    queryParams.ReplacementLevel = replacementLevel
                                                }
                                                api.get("/Recipe", 
                                                {params : queryParams,
                                                paramsSerializer: {
                                                    indexes: true,
                                                }}
                                                ).then(res => {
                                                    console.log(res.data)
                                                    setArray(res.data);
                                                });
                                            }}>Поиск</Button>
                                    </td>
                                    <td><Button 
                                    type="primary" 
                                    onClick={()=>{
                                        if(showAdditionalFilters === false){
                                            setShowAdditionalFilters(true)
                                        }
                                        else{
                                            setShowAdditionalFilters(false)
                                        }
                                    }}
                                    style={{ height:'60px', marginRight:'20px'}}> 
                                    
                                    Расширенная<br /> фильтрация
                                    </Button></td>
                    
                                </tr>
                            </tbody>
                        </table>
                    </td>
                    
                </tr>
            </tbody>
        </table>
        
        <div id='additional-filters' style={{display: showAdditionalFilters ? '' : 'none'}}>
        <table style={{ width: 1200, margin: "20px auto" }}>
            <tbody>
                <tr>
                    <td className="column-filter">
                        <label>Каллории на 100г.</label><br/>
                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <label>От</label>
                                    </td>
                                    <td>
                                        <Input
                                            className="filter-input-small-text"
                                            type="text"
                                            name = "caloriesMin"
                                            onChange={(e) => { setCaloriesMin(e.target.value) }} />
                                    </td>
                                    <td>
                                        <label>До</label>
                                    </td>
                                    <td>
                                        <Input
                                            className="filter-input-small-text"
                                            type="text"
                                            name = "caloriesMax"
                                            onChange={(e) => { setCaloriesMax(e.target.value) }} />
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>


                    
                    <td className="column-filter">
                        <label>Белки на 100г.</label><br/>
                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <label>От</label>
                                    </td>
                                    <td>
                                        <Input
                                            className="filter-input-small-text"
                                            type="text"
                                            name = "proteinsMin"
                                            onChange={(e) => { setProteinsMin(e.target.value) }} />
                                    </td>
                                    <td>
                                        <label>До</label>
                                    </td>
                                    <td>
                                        <Input
                                            className="filter-input-small-text"
                                            type="text"
                                            name = "proteinsMax"
                                            onChange={(e) => { setProteinsMax(e.target.value) }} />
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        
                    </td>

                    <td className="column-filter">
                        <label>Жиры на 100г.</label><br/>
                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <label>От</label>
                                    </td>
                                    <td>
                                        <Input
                                            className="filter-input-small-text"
                                            type="text"
                                            name = "fatsMin"
                                            onChange={(e) => { setFatsMin(e.target.value) }} />
                                    </td>
                                    <td>
                                        <label>До</label>
                                    </td>
                                    <td>
                                        <Input
                                            className="filter-input-small-text"
                                            type="text"
                                            name = "fatsMax"
                                            onChange={(e) => { setFatsMax(e.target.value) }} />
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>

                    <td className="column-filter">
                        <label>Углеводы на 100г.</label><br/>
                        <table>
                            <tbody>
                                <tr>
                                    <td>
                                        <label>От</label>
                                    </td>
                                    <td>
                                        <Input
                                            className="filter-input-small-text"
                                            type="text"
                                            name = "carbohydratesMin"
                                            onChange={(e) => { setCarbohydratesMin(e.target.value) }} />
                                    </td>
                                    <td>
                                        <label>До</label>
                                    </td>
                                    <td>
                                        <Input
                                            className="filter-input-small-text"
                                            type="text"
                                            name = "carbohydratesMax"
                                            onChange={(e) => { setCarbohydratesMax(e.target.value) }} />
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                    
                </tr>
            </tbody>
        </table>
            <div style={{margin:'10px 0 10px 30px'}}>Выберите продукты</div>
            <Select
            mode="multiple"
            allowClear
            showSearch={true} 
            optionFilterProp="label"
            style={{ width: '50%', display:'block'}}
            placeholder="Список продуктов"
            onChange={setProductsFilter}
            filterOption={true}
            maxCount={5}
            options={products}
            value={productsFilter}
            />           
          <div style={{margin:'10px 0 10px 30px'}}>Степень замещения</div>
          <Slider defaultValue={0}
          tooltipVisible={false}
          min={0}
          max={75}
          step={25}
          style={{display:'block', width:'40%',  textAlign:'left'}}
          onChange={onSliderChange}/>
          <span>{replacementLevelSpan}</span>    
        </div>

        <Table dataSource={recipes}
        pagination={{
            pageSize: 10
          }}

        onRow={(record, rowIndex) => {
            return {
              onClick: (event) => {
                if(record.isTest !== true){
                    navigate(`/recipes/${record.recipeId}`)
                }
            }, // click row
            };
          }}
        onChange={onColumn} columns={columns.filter(x => x.hide !== true)} rowKey="recipeId" >
            
          
        </Table >
        </div>
        );
}

export default ListRecipe;