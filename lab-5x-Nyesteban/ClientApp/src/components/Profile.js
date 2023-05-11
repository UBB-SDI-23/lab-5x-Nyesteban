import axios from "axios";
import { useEffect, useState, useMemo } from "react";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import { useLocation } from "react-router";
import { currentToken, currentlyLoggedIn, showCount } from './Login';

function Profile() {
    const [username, setUsername] = useState("");
    const [bio, setBio] = useState("");
    const [location, setLocation] = useState("");
    const [realName, setRealName] = useState("");
    const [eMail, setEMail] = useState("");
    const [website, setWebsite] = useState("");
    const [count, setCount] = useState(0);
    const [countApp, setCountApp] = useState(0);
    const [countCompany, setCountCompany] = useState(0);
    const [countDev, setCountDev] = useState(0);
    const [countGame, setCountGame] = useState(0);
    const [showCountHere, setShowCount] = useState(0);

    const config = {
        headers: { 'Authorization': 'Bearer ' + currentToken }
    };

    let data = useLocation();

    console.log(data.state.info);

    useEffect(() => {
        (async () => await getData())();
    }, []);

    async function getData() {

        const result = await axios.get("/api/Auth/getuser/" + data.state.info);
        const counter = await axios.get("/api/Auth/getuser/" + data.state.info + "/count");
        const counterapp = await axios.get("/api/Auth/getuser/" + data.state.info + "/appcount");
        const countercompany = await axios.get("/api/Auth/getuser/" + data.state.info + "/companycount");
        const counterdev = await axios.get("/api/Auth/getuser/" + data.state.info + "/devcount");
        const countergame = await axios.get("/api/Auth/getuser/" + data.state.info + "/gamecount");
        const showCounter = await axios.get("/api/Auth/getuser/" + data.state.info + "/showcount");
        setUsername(result.data.username);
        setBio(result.data.bio);
        setLocation(result.data.location);
        setRealName(result.data.realName);
        setEMail(result.data.eMail);
        setWebsite(result.data.website);
        setCount(counter.data);
        setCountApp(counterapp.data);
        setCountCompany(countercompany.data);
        setCountDev(counterdev.data);
        setCountGame(countergame.data);
        setShowCount(showCounter.data);
    }

    async function putShowCount(event) {
        event.preventDefault();
        try {
            await axios.put("/api/Auth/" + data.state.info + "/showcount/" + showCountHere, {}, config);
            toast("Show count updated.");
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
                        <label>Entitites Count</label>
                        <input
                            type="text"
                            class="form-control"
                            id="count"
                            value={count}
                            onChange={(event) => {
                                setCount(event.target.value);
                            }}
                        />
                    </div>
                    <div class="form-group">
                        <label>App Count</label>
                        <input
                            type="text"
                            class="form-control"
                            id="countApp"
                            value={countApp}
                            onChange={(event) => {
                                setCountApp(event.target.value);
                            }}
                        />
                    </div>
                    <div class="form-group">
                        <label>Company Count</label>
                        <input
                            type="text"
                            class="form-control"
                            id="countCompany"
                            value={countCompany}
                            onChange={(event) => {
                                setCountCompany(event.target.value);
                            }}
                        />
                    </div>
                    <div class="form-group">
                        <label>DevelopmentDetails Count</label>
                        <input
                            type="text"
                            class="form-control"
                            id="countDev"
                            value={countDev}
                            onChange={(event) => {
                                setCountDev(event.target.value);
                            }}
                        />
                    </div>
                    <div class="form-group">
                        <label>Game Count</label>
                        <input
                            type="text"
                            class="form-control"
                            id="countGame"
                            value={countGame}
                            onChange={(event) => {
                                setCountGame(event.target.value);
                            }}
                        />
                    </div>
                    <div class="form-group">
                        <label>Show Count</label>
                        <input
                            type="text"
                            class="form-control"
                            id="showCountHere"
                            value={showCountHere}
                            onChange={(event) => {
                                if (currentlyLoggedIn == username)
                                    setShowCount(event.target.value);
                                else
                                    toast("I'm afraid I can't let you do that.");
                            }}
                        />
                    </div>
                    <div>
                        <button class="btn btn-primary mt-4" onClick={putShowCount}>
                            Set new show count
                        </button>
                    </div>
                </form>
            </div>
            <br></br>


            <ToastContainer />
        </div>
    );
}

export default Profile;

export { showCount };