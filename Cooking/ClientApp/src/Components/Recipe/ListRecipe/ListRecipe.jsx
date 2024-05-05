import { Button, Input, Table, Select, Radio  } from 'antd';
import React, { useEffect, useState } from 'react';
import api from "../../../api/api";
import './ListRecipe.css';

const ListRecipe = () => {

    const [recipes, setArray] = useState([]);

    const [productsFilter, setProductsFilter] = useState([]);

    const [products, setProducts] = useState('');

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


    const columns = [
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
        },
        ];

    useEffect(() => {
        api.get("/Recipe", {params : {PageNumber : 10}}).then(res => {
            setArray(res.data);
            console.log(res.data);
        });

        api.get("/Product").then(res => {
            let list = res.data.map(x => ({
              value:x.productId,
              label:x.name}));
            setProducts(list);
        });

    }, []);

    function onRadioGroupChange(e){
        let value = e.target.value
        setRadioGroupValue(value);
        if(value === 1){
            setReplacementLevel(null)
        }
        if(value === 2){
            setReplacementLevel("Low")
        }
        if(value === 3){
            setReplacementLevel("Medium")
        }
        if(value === 4){
            setReplacementLevel("Hard")
        }
    }

    return (
        <div>
        <table style={{ width: 1200, margin: "20px auto" }}>
            <tbody>
                <tr>
                    <td><Button type="primary" style={{ height:'60px', marginRight:'20px'}}> Расширенная<br /> фильтрация</Button></td>
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
                    
                    <td>
                        <Button style={{marginTop:'20px'}} type="primary"
                            onClick={() => {
                                api.get("/Recipe", 
                                {params : {
                                    CaloriesMin : caloriesMin,
                                    CaloriesMax : caloriesMax,
                                    ProteinsMin : proteinsMin,
                                    ProteinsMax : proteinsMax,
                                    FatsMin : fatsMin,
                                    FatsMax : fatsMax,
                                    CarbohydratesMin : carbohydratesMin,
                                    CarbohydratesMax : carbohydratesMax,
                                    Products : productsFilter,
                                    ReplacementLevel : replacementLevel
                                },
                                paramsSerializer: {
                                    indexes: true,
                                }}
                                ).then(res => {
                                    setArray(res.data);
                                    console.log(res.data);
                                });
                            }}>Поиск</Button>
                    </td>
                    
                </tr>
            </tbody>
        </table>
        
        <div id='additional-filters'>
            <div style={{margin:'10px 0 10px 30px'}}>Выберите продукты</div>
            <Select
            mode="multiple"
            allowClear
            style={{ width: '50%', display:'block'}}
            placeholder="Список продуктов"
            onChange={setProductsFilter}
            maxCount={5}
            options={products}
            value={productsFilter}
            /> 
            <div style={{display:'block'}}>
                <div style={{margin:'20px 0 10px 30px'}}>Выберите степень замещения</div>
                <Radio.Group style={{display:'block', width:'50%',  textAlign:'left'}} onChange={onRadioGroupChange} value={radioGroupValue}>
                    <Radio value={1}>Не учитывать</Radio>
                    <Radio value={2}>Слабо</Radio>
                    <Radio value={3}>Средне</Radio>
                    <Radio value={4}>Сильно</Radio>
                </Radio.Group> 
            </div>       
        </div>

        <Table dataSource={recipes} columns={columns.filter(x => x.hide !== true)} rowKey="recipeId" >
            

        </Table >
        </div>
        );
}

export default ListRecipe;