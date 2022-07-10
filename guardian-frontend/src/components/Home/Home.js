import { React, useContext, useState, useEffect, useRef, useCallback } from "react";
import { Link, useLocation, useNavigate } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faSearch, faSignOutAlt, faList, faChevronCircleDown } from "@fortawesome/free-solid-svg-icons";

import axiosPrivate from "../../api/axios";
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
    const [selectedCategory, setCategory] = useState('');
    const [games, setGames] = useState([]);

    useEffect(() => {
        let isMounted = true;
        const controller = new AbortController();

        const getCategories = async () => {
            try {

                const response = await axiosPrivate.get(`/v1.0/Categories`, {
                    headers: {
                        'Authorization': `${token}`
                    }
                });
                console.log(response.data);

                isMounted && setCategories(Array.from(response.data));
            } catch (e) {
                console.log(e);
                navigate('/login', { state: { from: location }, replace: true })
            }
        }
        if (!categories.length)
            getCategories();

        const getGamesForCategory = async (category) => {
            console.log(category);
            try {
                const response = await axiosPrivate.get(`/v1.0/Categories/games/${category}`, {
                    headers: {
                        'Authorization': `${token}`
                    }
                });
                console.log(response.data);

                var mappedGames = Object.entries(response.data).map(([key, value]) =>
                    <div className="game" onClick={selectGame}>
                        <h4>{value.name}</h4>
                        <img src={value.imageUrl} alt="Game Logo" className="image" />
                        <div className="overlay">{value.description}</div>
                    </div>)

                setGames(mappedGames);

            } catch (e) {
                console.log(e);
            }
        }

        if (typeof selectedCategory !== 'undefined' && selectedCategory !== '')
            getGamesForCategory(selectedCategory);

        return () => {
            isMounted = false;
            controller.abort();
        }
    }, [selectedCategory])


    const logout = async () => {
        setAuth({});
        navigate('/login');
    }

    const addGame = async () => {

    }

    const selectGame = async () => {
        navigate('/game')
    }

    return (
        <section className="home-wrapper">

            <div className="top">
                <img src={require('../../assets/logo.png')}
                    className="logo"
                    alt="brand-logo" />
                <nav>
                    <ol>
                        <li>
                            <button onClick={addGame} className="add-game">Add game</button>
                        </li>
                        <li>
                            <button onClick={logout} className="logout">Logout</button>
                        </li>
                    </ol>
                </nav>
            </div>
            <div className="content">
                <header className="categories-wrapper">
                    <ul className="category-list">
                        {categories.map((category) =>
                            <li
                                key={category.categoryName}
                                id={`category${category.id}`}
                                className="category"
                            >
                                <a href={`#` + category.categoryName}
                                    onClick={(e) => { setCategory(category.categoryName) }}>
                                    {category.categoryName}
                                </a>
                            </li>
                        )}
                    </ul>
                </header>

                <header className="games-wrapper">
                    {games.map((game) => game)}
                </header>
            </div>
        </section >




    );
}


export default Home;
