import axios from "axios";
import { useEffect, useState, useMemo } from "react";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

let currentlyLoggedIn = "";

function Login() {
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");

    async function login(event) {

        event.preventDefault();
        try {
            await axios.post("https://nyesteban.twilightparadox.com/api/Auth/login", {

                Username: username,
                Password: password

            });
            setUsername("");
            setPassword("");
            toast("Login succesful!");
            currentlyLoggedIn = username;
            
        } catch (err) {
            toast(err.message);
            //alert(err);
        }
    }

    return (
        <div>
            <h1>Login</h1>
            <div class="container mt-4">
                <form>
                    <div class="form-group">
                        <label>Username</label>
                        <input
                            type="text"
                            class="form-control"
                            id="username"
                            value={username}
                            onChange={(event) => {
                                setUsername(event.target.value);
                            }}
                        />
                    </div>
                    <div class="form-group">
                        <label>Password</label>
                        <input
                            type="text"
                            class="form-control"
                            id="password"
                            value={password}
                            onChange={(event) => {
                                setPassword(event.target.value);
                            }}
                        />
                    </div>
                    <div>
                        <button class="btn btn-primary mt-4" onClick={login}>
                            Login
                        </button>
                    </div>
                </form>
            </div>
            <br></br>


            <ToastContainer />
        </div>
    );
}

export default Login;

export { currentlyLoggedIn };