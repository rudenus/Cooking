import { Layout, Menu } from 'antd';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';
import { Link } from "react-router-dom";
import { logout } from '../../data/actionCreators/LoginActions';
const { Header, Content, Footer } = Layout;


const AuthorizedLayout = ({ children, logout }) => {
    return (
        <Layout>
            <Header style={{ position: 'sticky', top: 0, zIndex: 1, width: '100%' }}>
                <Menu
                    defaultSelectedKeys={['1']}
                    theme="dark"
                    mode="horizontal"
                >
                    <Menu.Item key="2">
                        <span>Создать рецепт</span>
                        <Link to="/recipes/create" />
                    </Menu.Item>
                    <Menu.Item key="3">
                        <span>Авторизация</span>
                        <Link to="/login" />
                    </Menu.Item>
                    <Menu.Item key="4">
                        <span>Рецепты</span>
                        <Link to="/recipes" />
                    </Menu.Item>
                    <Menu.Item key="5">
                        <span>Регистрация</span>
                        <Link to="/register" />
                    </Menu.Item>
                </Menu>
            </Header>
            <Content className="site-layout" style={{ padding: '0 50px' }}>
                {children}
            </Content>
            <Footer style={{ textAlign: 'center' }}></Footer>
        </Layout>
    );
}

AuthorizedLayout.propTypes = {
    logout: PropTypes.func.isRequired
}

export default connect((state) => { return {} }, { logout })(AuthorizedLayout);