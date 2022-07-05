import React from 'react';
import { Routes, Route } from 'react-router-dom';
import Layout from "./components/Layout";
import Login from "./components/Login/Login";
import './App.css';
import './index.css';
import Register from "./components/Register/Register";

function App() {
  return (
      <Routes>
        <Route path="/" element={<Layout />}>
            <Route path="/login" element={<Login/>}/>
            <Route path="/register" element={<Register/>}/>
        </Route>
      </Routes>
  );
}

export default App;
