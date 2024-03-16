import { Button, Input, Table } from 'antd';
import React, { useEffect, useState } from 'react';
import api from "../../../api/api";
import './ListRecipe.css';
const { Column } = Table;

const ListRecipe = () => {

    const [recipes, setArray] = useState([]);
    const [item, setItem] = useState({});

    const [caloriesMin, setCaloriesMin] = useState('');
    const [caloriesMax, setCaloriesMax] = useState('');
    const [proteinsMin, setProteinsMin] = useState('');
    const [proteinsMax, setProteinsMax] = useState('');
    const [fatsMin, setFatsMin] = useState('');
    const [fatsMax, setFatsMax] = useState('');
    const [carbohydratesMin, setCarbohydratesMin] = useState('');
    const [carbohydratesMax, setCarbohydratesMax] = useState('');


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
            title: 'Каллории',
            dataIndex: 'calories',
            key: 'calories',
        },
        {
            title: 'Белки',
            dataIndex: 'proteins',
            key: 'proteins',
        },
        {
            title: 'Жиры',
            dataIndex: 'fats',
            key: 'fats',
        },
        {
            title: 'Углеводы',
            dataIndex: 'carbohydrates',
            key: 'carbohydrates',
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
    }, []);

    return (
        <div>
        <table style={{ width: 1200, margin: "20px auto" }}>
            <tbody>
                <tr>
                    <td className="column-filter">
                        <label>Каллории</label><br/>
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
                        <label>Белки</label><br/>
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
                                            onChange={(e) => { setCaloriesMax(e.target.value) }} />
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        
                    </td>

                    <td className="column-filter">
                        <label>Жиры</label><br/>
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
                        <label>Углеводы</label><br/>
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
                                    CarbohydratesMax : carbohydratesMax
                                
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

        <Table dataSource={recipes} columns={columns.filter(x => x.hide !== true)} rowKey="recipeId" >
            

        </Table >
        </div>
        );
}

export default ListRecipe;