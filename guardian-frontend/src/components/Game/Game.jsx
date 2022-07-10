import { React, useContext, useState, useEffect, useRef, useCallback, useSearchParams } from "react";
import { Link, useLocation, useNavigate } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faSearch, faSignOutAlt, faList, faChevronCircleDown } from "@fortawesome/free-solid-svg-icons";

import axiosPrivate from "../../api/axios";
import "./Game.scss";

import AuthContext from "../../context/AuthProvider";

import useAuth from "../../hooks/useAuth";

const Game = () => {
    const [gameInfo, setGameInfo] = useState();

    const token = localStorage.getItem('token');

    useEffect(() => {

        getGameInfo();

    }, [])

    function getQueryParams() {
        return new URLSearchParams(window.location.search);
    }

    const getGameInfo = async () => {
        try {
            let gameId = getQueryParams().get('id');

            const response = await axiosPrivate.get(`/v1.0/Games/${gameId}`, {
                headers: {
                    'Authorization': `${token}`
                }
            });

            console.log(response.data);
            if (response.status !== 200) {
                return <h3>{response.data.Message}</h3>
            }

            var game = response.data;
            var categories = game.categories.map(x => x.categoryName).join(', ')
            setGameInfo(<div>
                <div>
                    <img src={game.imageUrl} />
                </div>
                <div>
                    <h3>Game Name: </h3>{game.name}
                </div>
                <div>
                    <h3>Game Description: </h3>{game.description}
                </div>
                <div>
                    <h3>Game License: </h3>{game.license}
                </div>
                <div>
                    <h3>Game categories: </h3>{categories}
                </div>
            </div >);

        } catch (e) {
            console.log(e);
        }
    }

    return <div>
        {gameInfo}
    </div>
}

export default Game;