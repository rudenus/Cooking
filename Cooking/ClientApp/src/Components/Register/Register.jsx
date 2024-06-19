import { InfoCircleOutlined, UserOutlined } from '@ant-design/icons';
import { Button, Input, Tooltip } from 'antd';
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import api from "../../api/api";
const Register
    = () => {
        const [phone, setPhone] = useState('');
        const [email, setEmail] = useState('');
        const [login, setLogin] = useState('');
        const [password, setPassword] = useState('');
        const [errorMessage, setErrorMessage] = useState('');

        let navigate = useNavigate();
        return (
            <div style={{ width: 500, margin: "auto" }}>
                <Input
                    onChange={(e) => { setPhone(e.target.value) }}
                    style={{ marginTop: "15px" }}
                    placeholder="Телефон"
                    suffix={
                        <Tooltip title="Введите телефон">
                            <InfoCircleOutlined style={{ color: 'rgba(0,0,0,.45)' }} />
                        </Tooltip>
                    }
                />

                <Input
                    onChange={(e) => { setEmail(e.target.value) }}
                    style={{ marginTop: "15px" }}
                    placeholder="Почта"
                    suffix={
                        <Tooltip title="Введите почту">
                            <InfoCircleOutlined style={{ color: 'rgba(0,0,0,.45)' }} />
                        </Tooltip>
                    }
                />

                <Input
                    onChange={(e) => { setLogin(e.target.value) }}
                    style={{ marginTop: "15px" }}
                    placeholder="Логин"
                    suffix={
                        <Tooltip title="Введите логин">
                            <InfoCircleOutlined style={{ color: 'rgba(0,0,0,.45)' }} />
                        </Tooltip>
                    }
                />

                <Input.Password onChange={(e) => { setPassword(e.target.value) }} placeholder="Введите пароль" style={{ marginTop: "15px", marginBottom:"15px" }} />

                <Button type="primary" onClick={() => {
                    api.post('/account/register',
                        {Phone : phone, Email : email, Login: login, Password: password }).then((res) => { navigate('/login') }).catch((error) => { setErrorMessage(error.response.data) });
                }} block style={{ marginTop: "5px" }}>
                    Зарегистрироваться
                </Button>
                {errorMessage && <div className="error" style={{ color: "red" }}> {errorMessage} </div>}
            </div>
        );
    }

export default Register;