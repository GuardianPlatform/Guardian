import { useRef, useState, useEffect } from "react";
import { Link, useNavigate, useLocation } from 'react-router-dom';
import useAuth from "../../hooks/useAuth";
import "./Login.css";

import axios from "../../api/axios";
const LOGIN_URL = "account/login";

const Login = () => {

    const {setAuth } = useAuth();

    const navigate = useNavigate();
    const location = useLocation();
    const from = location.state?.from?.pathname || "/";


    const userRef = useRef();
    const errRef = useRef();

    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [errMsg, setErrMsg] = useState('');


    useEffect(() => {
        userRef.current.focus();
    }, []);

    useEffect(() => {
        setErrMsg('');
    }, [email,password]);



    const handleSubmit = async (e) => {
        e.preventDefault();

        try {
            const response = await axios.post(LOGIN_URL,
                JSON.stringify({email, password}),
                {
                    headers: {'Content-Type' : 'application/json'},
                    withCredentials: true
                }
            );
            console.log(JSON.stringify(response?.data));
            const token = response?.data;
            localStorage.setItem('token', `Bearer ${token}`);
            setAuth({email, password, token});
            setEmail('');
            setPassword('');
            navigate(from, {replace: true});
            console.log(`access token to: ${token}`);
        } catch (e) {
            if (!e?.response) {
                setErrMsg("No Server Response");
            } else if (e.response?.status === 400) {
                setErrMsg('Missing Email or Password');
            } else {
                setErrMsg('Login Failed');
            }
            errRef.current.focus();
        }

    }
    return (

        <section>
            <img src={require('../../assets/logo.png')} className="logo" alt="brand-logo"/>
            <h1>Guardian Games</h1>
            <form onSubmit={handleSubmit}>
                <label htmlFor="email">Email:</label>
                <input
                    type="text"
                    id="email"
                    ref={userRef}
                    autoComplete="off"
                    onChange={(e) => setEmail(e.target.value)}
                    value={email}
                    required
                />

                <label htmlFor="password">Password:</label>
                <input
                    type="password"
                    id="password"
                    onChange={(e) => setPassword(e.target.value)}
                    value={password}
                    required
                />
                <button>Sign In</button>
            </form>
            <p>
                You don't have an account yet? <br/>
                <Link to="/register">Sign Up</Link>
            </p>
        </section>

    );
}

export default Login;