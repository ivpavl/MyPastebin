import React from 'react';
import { Route, Routes } from 'react-router-dom';
import AppRoutes from './AppRoutes';
import Layout from './components/Layout';
import './styles/custom.css';

function App()
{
  return (
    <Layout>
      <Routes>
        {AppRoutes.map((route, _) => {
          const { element, ...rest } = route;
          return <Route element={element} {...rest} />;
        })}
      </Routes>
    </Layout>
  );

}
export default App;
