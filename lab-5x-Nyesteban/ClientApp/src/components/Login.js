import axios from "axios";
import { useEffect, useState, useMemo } from "react";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

let currentlyLoggedIn = "";
let currentToken = "";
let currentUserId = 0;
let currentRole = "";
let showCount = 12;

function Login() {
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");

    async function login(event) {

        event.preventDefault();
        try {
            await axios.post("/api/Auth/login", {

                Username: username,
                Password: password

            }).then(function (response) {
                currentToken = response.data;
                console.log(response.data);
            });
            setUsername("");
            setPassword("");
            toast("Login succesful!");
            await axios.get("/api/Auth/getuserid/" + username).then(function (response) {
                currentUserId = response.data;
                console.log(response.data);
            });
            await axios.get("/api/Auth/getuserrole/" + username).then(function (response) {
                currentRole = response.data;
                console.log(response.data);
            });
            await axios.get("/api/Auth/getusershowcount/" + username).then(function (response) {
                showCount = response.data;
                console.log(response.data);
            });
            currentlyLoggedIn = username;
            
        } catch (err) {
            toast(err.message);
            //alert(err);
        }
    }

    async function updateShow(event) {

        event.preventDefault();
        try {
            await axios.get("/api/Auth/getusershowcount/" + currentlyLoggedIn).then(function (response) {
                showCount = response.data;
                console.log(response.data);
            });
            toast("Show count changed.");
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
                    <div>
                        <button class="btn btn-primary mt-4" onClick={updateShow}>
                            Update show count
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

export { currentlyLoggedIn, currentToken, currentUserId, currentRole, showCount };