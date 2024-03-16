import { InfoCircleOutlined, UserOutlined } from '@ant-design/icons';
import { Button, Input, Tooltip } from 'antd';
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import api from "../../api/api";
const Register
    = () => {
        const [name, setName] = useState('');
        const [surname, setSurname] = useState('');
        const [patronymic, setPatronymic] = useState('');
        const [phone, setPhone] = useState('');
        const [email, setEmail] = useState('');
        const [login, setLogin] = useState('');
        const [password, setPassword] = useState('');
        const [errorMessage, setErrorMessage] = useState('');

        let navigate = useNavigate();
        return (
            <div style={{ width: 500, margin: "auto" }}>
                <Input
                    onChange={(e) => { setName(e.target.value) }}
                    style={{ marginTop: "5px" }}
                    placeholder="Name"
                    suffix={
                        <Tooltip title="Enter your name">
                            <InfoCircleOutlined style={{ color: 'rgba(0,0,0,.45)' }} />
                        </Tooltip>
                    }
                />

                <Input
                    onChange={(e) => { setSurname(e.target.value) }}
                    style={{ marginTop: "5px" }}
                    placeholder="Surname"
                    suffix={
                        <Tooltip title="Enter your surname">
                            <InfoCircleOutlined style={{ color: 'rgba(0,0,0,.45)' }} />
                        </Tooltip>
                    }
                />

                <Input
                    onChange={(e) => { setPatronymic(e.target.value) }}
                    style={{ marginTop: "5px" }}
                    placeholder="Patronymic"
                    suffix={
                        <Tooltip title="Enter your patronymic">
                            <InfoCircleOutlined style={{ color: 'rgba(0,0,0,.45)' }} />
                        </Tooltip>
                    }
                />

                <Input
                    onChange={(e) => { setPhone(e.target.value) }}
                    style={{ marginTop: "5px" }}
                    placeholder="Phone"
                    suffix={
                        <Tooltip title="Enter your phone">
                            <InfoCircleOutlined style={{ color: 'rgba(0,0,0,.45)' }} />
                        </Tooltip>
                    }
                />

                <Input
                    onChange={(e) => { setEmail(e.target.value) }}
                    style={{ marginTop: "5px" }}
                    placeholder="Email"
                    suffix={
                        <Tooltip title="Enter your email">
                            <InfoCircleOutlined style={{ color: 'rgba(0,0,0,.45)' }} />
                        </Tooltip>
                    }
                />

                <Input
                    onChange={(e) => { setLogin(e.target.value) }}
                    style={{ marginTop: "5px" }}
                    placeholder="Login"
                    suffix={
                        <Tooltip title="Enter your login">
                            <InfoCircleOutlined style={{ color: 'rgba(0,0,0,.45)' }} />
                        </Tooltip>
                    }
                />

                <Input.Password onChange={(e) => { setPassword(e.target.value) }} placeholder="Input password" style={{ marginTop: "5px" }} />

                <Button type="primary" onClick={() => {
                    api.post('/account/register',
                        {Name : name, Surname : surname, Patronymic : patronymic, Phone : phone, Email : email, Login: login, Password: password }).then((res) => { navigate('/login') }).catch((error) => { setErrorMessage(error.response.data) });
                }} block style={{ marginTop: "5px" }}>
                    Зарегистрироваться
                </Button>
                {errorMessage && <div className="error" style={{ color: "red" }}> {errorMessage} </div>}
            </div>
        );
    }

export default Register;