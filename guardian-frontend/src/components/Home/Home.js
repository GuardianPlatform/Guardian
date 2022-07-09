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


    const token = localStorage.getItem('token');

    const [categories, setCategories] = useState([]);



    useEffect(() => {
        let isMounted = true;
        const controller = new AbortController();

        const getCategories = async () => {
            try {

                const response = await axiosPrivate.get(`/v1.0/Categories`, {
                    headers : {
                        'Authorization' : `${token}`
                    }
                });
                console.log(response.data);
                console.log(auth);
                isMounted && setCategories(Array.from(response.data));
            } catch (e) {
                console.log(e);
                navigate('/login', {state: {from: location}, replace: true})
            }
        }
        getCategories();

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
            </div>
        </header>
        {/* HEADER END*/}
        <header className="categories-wrapper">
            <ul className="category-list">
                {categories.map((category) =>
                    <li
                    key={"category"}
                    id={`category${category.id}`}
                    className={"category"}
                    >
                        {category.categoryName}
                    </li>
                )}
            </ul>
        </header>
    </section>




);
}


export default Home;
