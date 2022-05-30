import React from "react";
import "./Login.scss";

const Login = () => {
    return (

        <section>
            <img src={require('../../assets/logo.png')} className="logo" alt="brand-logo"/>
            <h1>Guardian Games</h1>
            <form>
                <label>Email:</label>
                <input
                    type="text"
                    id="email"
                    autoComplete="off"
                    required
                />

                <label htmlFor="password">Password:</label>
                <input
                    type="password"
                    id="password"
                    required
                />
                <button>Sign In</button>
            </form>
        </section>

    );
}

export default Login;