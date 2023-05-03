import axios from "axios";
import { useEffect, useState, useMemo } from "react";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

function Register() {
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [bio, setBio] = useState("");
    const [location, setLocation] = useState("");
    const [realName, setRealName] = useState("");
    const [eMail, setEMail] = useState("");
    const [website, setWebsite] = useState("");
    const [code, setCode] = useState("");

    async function register(event) {

        event.preventDefault();
        if (bio.length <= 3) {
            toast("Bio is too short!");
            return;
        }
        if (location.length <= 3) {
            toast("Location is too short!");
            return;
        }
        if (realName.length <= 1) {
            toast("Name is too short!");
            return;
        }
        if (eMail.length <= 3) {
            toast("E-Mail is too short!");
            return;
        }
        if (website.length <= 2) {
            toast("Website is too short!");
            return;
        }
        if (eMail.toLowerCase().includes('@') == false) {
            toast("E-Mail must contain at least one @!");
            return;
        }
        if (website.toLowerCase().includes('.') == false) {
            toast("E-Mail must contain at least one .!");
            return;
        }
        try {
            await axios.post("/api/Auth/register", {

                Username: username,
                Password: password,
                Bio: bio,
                Location: location,
                RealName: realName,
                EMail: eMail,
                Website: website

            }).then(response => { setCode(response.data); });
        } catch (err) {
            toast(err.message);
            //alert(err);
        }
    }

    async function registerConfirm(event) {

        event.preventDefault();
        if (bio.length <= 3) {
            toast("Bio is too short!");
            return;
        }
        if (location.length <= 3) {
            toast("Location is too short!");
            return;
        }
        if (realName.length <= 1) {
            toast("Name is too short!");
            return;
        }
        if (eMail.length <= 3) {
            toast("E-Mail is too short!");
            return;
        }
        if (website.length <= 2) {
            toast("Website is too short!");
            return;
        }
        if (eMail.toLowerCase().includes('@') == false) {
            toast("E-Mail must contain at least one @!");
            return;
        }
        if (website.toLowerCase().includes('.') == false) {
            toast("E-Mail must contain at least one .!");
            return;
        }
        try {
            await axios.post("/api/Auth/register/confirm/" + code, {

                Username: username,
                Password: password,
                Bio: bio,
                Location: location,
                RealName: realName,
                EMail: eMail,
                Website: website

            });
            toast("Registration successful.");
            setUsername("");
            setPassword("");
            setBio("");
            setLocation("");
            setRealName("");
            setEMail("");
            setWebsite("");
        } catch (err) {
            toast(err.message);
            //alert(err);
        }
    }

    return (
        <div>
            <h1>Register</h1>
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
                    <div class="form-group">
                        <label>Bio</label>
                        <input
                            type="text"
                            class="form-control"
                            id="bio"
                            value={bio}
                            onChange={(event) => {
                                setBio(event.target.value);
                            }}
                        />
                    </div>
                    <div class="form-group">
                        <label>Location</label>
                        <input
                            type="text"
                            class="form-control"
                            id="location"
                            value={location}
                            onChange={(event) => {
                                setLocation(event.target.value);
                            }}
                        />
                    </div>
                    <div class="form-group">
                        <label>Real Name</label>
                        <input
                            type="text"
                            class="form-control"
                            id="realName"
                            value={realName}
                            onChange={(event) => {
                                setRealName(event.target.value);
                            }}
                        />
                    </div>
                    <div class="form-group">
                        <label>E-Mail</label>
                        <input
                            type="text"
                            class="form-control"
                            id="eMail"
                            value={eMail}
                            onChange={(event) => {
                                setEMail(event.target.value);
                            }}
                        />
                    </div>
                    <div class="form-group">
                        <label>Website</label>
                        <input
                            type="text"
                            class="form-control"
                            id="website"
                            value={website}
                            onChange={(event) => {
                                setWebsite(event.target.value);
                            }}
                        />
                    </div>
                    <div class="form-group">
                        <label>Code</label>
                        <input
                            type="text"
                            class="form-control"
                            id="code"
                            value={code}
                            onChange={(event) => {
                                setCode(event.target.value);
                            }}
                        />
                    </div>
                    <div>
                        <button class="btn btn-primary mt-4" onClick={register}>
                            Request code
                        </button>
                        <button class="btn btn-warning mt-4" onClick={registerConfirm}>
                            Register with code
                        </button>
                    </div>
                </form>
            </div>
            <br></br>


            <ToastContainer />
        </div>
    );
}

export default Register;