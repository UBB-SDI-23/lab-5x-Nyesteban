import axios from "axios";
import { useEffect, useState, useMemo } from "react";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import { currentToken, currentUserId } from './Login';

function Admin() {
    const [username, setUsername] = useState("");
    const [role, setRole] = useState("");
    const [showCountHere, setShowCount] = useState(12);

    const config = {
        headers: { 'Authorization': 'Bearer ' + currentToken }
    };

    async function roleChanger(event) {

        event.preventDefault();
        try {
            await axios.put("https://nyesteban.twilightparadox.com/api/Auth/updateroles/" + username + "/" + role, {}, config);
            setUsername("");
            setRole("");
            toast("Role changed.");
        } catch (err) {
            toast(err.message);
            //alert(err);
        }
    }

    async function putShowCount(event) {

        event.preventDefault();
        try {
            await axios.put("https://nyesteban.twilightparadox.com/api/Auth/updatecount/" + username + "/showcount/" + showCountHere, {}, config);
            setUsername("");
            setShowCount("");
            toast("Show count changed.");
        } catch (err) {
            toast(err.message);
            //alert(err);
        }
    }

    async function bulkDeleteGames(event) {

        event.preventDefault();
        try {
            await axios.delete("https://https://nyesteban.twilightparadox.com/api/Games/bulk/", config);
            toast("Games bulk deleted.");
        } catch (err) {
            toast(err.message);
            //alert(err);
        }
    }

    async function bulkDeleteCompanies(event) {

        event.preventDefault();
        try {
            await axios.delete("https://nyesteban.twilightparadox.com/api/Companies/bulk/", config);
            toast("Companies bulk deleted.");
        } catch (err) {
            toast(err.message);
            //alert(err);
        }
    }

    async function bulkDeleteDevelopmentDetails(event) {

        event.preventDefault();
        try {
            await axios.delete("https://nyesteban.twilightparadox.com/api/DevelopmentDetails/bulk/", config);
            toast("DevelopmentDetails bulk deleted.");
        } catch (err) {
            toast(err.message);
            //alert(err);
        }
    }

    async function bulkDeleteApps(event) {

        event.preventDefault();
        try {
            await axios.delete("https://nyesteban.twilightparadox.com/api/Apps/bulk/", config);
            toast("Apps bulk deleted.");
        } catch (err) {
            toast(err.message);
            //alert(err);
        }
    }

    async function bulkAddGames(event) {

        event.preventDefault();
        try {
            await axios.post("https://nyesteban.twilightparadox.com/api/Games/bulk/", {}, config);
            toast("Games generated.");
        } catch (err) {
            toast(err.message);
            //alert(err);
        }
    }

    return (
        <div>
            <h1>Admin Menu</h1>
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
                        <label>Role</label>
                        <input
                            type="text"
                            class="form-control"
                            id="role"
                            value={role}
                            onChange={(event) => {
                                setRole(event.target.value);
                            }}
                        />
                    </div>
                    <div>
                        <button class="btn btn-primary mt-4" onClick={roleChanger}>
                            Change Role
                        </button>
                    </div>
                    <div>
                        <button class="btn btn-primary mt-4" onClick={bulkDeleteGames}>
                            Bulk Delete Games
                        </button>
                    </div>
                    <div>
                        <button class="btn btn-primary mt-4" onClick={bulkDeleteCompanies}>
                            Bulk Delete Companies
                        </button>
                    </div>
                    <div>
                        <button class="btn btn-primary mt-4" onClick={bulkDeleteDevelopmentDetails}>
                            Bulk Delete DevelopmentDetails
                        </button>
                    </div>
                    <div>
                        <button class="btn btn-primary mt-4" onClick={bulkDeleteApps}>
                            Bulk Delete Apps
                        </button>
                    </div>
                    <div>
                        <button class="btn btn-primary mt-4" onClick={bulkAddGames}>
                            Generate Games
                        </button>
                    </div>
                    <div class="form-group">
                        <label>Show Count</label>
                        <input
                            type="text"
                            class="form-control"
                            id="showCountHere"
                            value={showCountHere}
                            onChange={(event) => {
                                setShowCount(event.target.value);
                            }}
                        />
                    </div>
                    <div>
                        <button class="btn btn-primary mt-4" onClick={putShowCount}>
                            Set Show Count For User
                        </button>
                    </div>
                </form>
            </div>
            <br></br>


            <ToastContainer />
        </div>
    );
}

export default Admin;