import { Layout, Menu } from 'antd';
import { Link } from "react-router-dom";
const { Header, Content, Footer } = Layout;


const NonAuthorizedLayout = ({ children }) => {
    return (
        <Layout>
            <Header style={{ position: 'sticky', top: 0, zIndex: 1, width: '100%' }}>
                <Menu
                    defaultSelectedKeys={['1']}
                    theme="dark"
                    mode="horizontal"
                >
                    <Menu.Item key="4">
                        <span>Рецепты</span>
                        <Link to="/recipes" />
                    </Menu.Item>
                    <Menu.Item key="3">
                        <span>Авторизация</span>
                        <Link to="/login" />
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

export default NonAuthorizedLayout;