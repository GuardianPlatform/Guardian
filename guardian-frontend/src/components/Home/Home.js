import {React, useContext, useState, useEffect, useRef} from "react";
import {Link, useLocation, useNavigate} from "react-router-dom";
import  {FontAwesomeIcon}  from "@fortawesome/react-fontawesome";
import {faSearch, faSignOutAlt, faList, faChevronCircleDown} from "@fortawesome/free-solid-svg-icons";

import {axiosPrivate} from "../../api/axios";
import "./Home.scss";

import AuthContext from "../../context/AuthProvider";

import useAuth from "../../hooks/useAuth";




const Home = () => {
    const { auth } = useAuth();
    const { setAuth } = useContext(AuthContext);
    const navigate = useNavigate();
    const location = useLocation();
    const searchRef = useRef();

    const token = localStorage.getItem('token');

    const [category, setCategory] = useState('');



    useEffect(() => {
        let isMounted = true;
        const controller = new AbortController();

        const getCategory = async () => {
            try {

                const response = await axiosPrivate.get(`/category`, {
                    headers : {
                        'Authorization' : `${token}`
                    }
                });
                console.log(response.data);
                console.log(auth);
                isMounted && setCategory(response.data);
            } catch (e) {
                console.log(e);
                navigate('/login', {state: {from: location}, replace: true})
            }
        }
        getCategory();

        return () => {
            isMounted = false;
            controller.abort();
        }
    }, [])

    const logout = async () => {
        setAuth({});
        navigate('/login');
    }

return (
    <section className="home-wrapper">

        {/*HEADER*/}
        <header>
            <img src={require('../../assets/logo.png')}
                 className="logo"
                 alt="brand-logo"/>




            <div className="logout">
                <button onClick={logout} className="logout">Logout</button>
               <FontAwesomeIcon icon={faSignOutAlt} onClick={logout}/>
            </div>
        </header>

        {/* HEADER END*/}




    </section>
);
}


export default Home;
