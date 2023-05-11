import axios from "axios";
import { useEffect, useState, useMemo } from "react";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import Pagination from './Pagination';
import Dropdown from 'react-bootstrap/Dropdown';
import DropdownButton from 'react-bootstrap/DropdownButton';
import { currentToken, currentUserId } from './Login';

function DevelopmentDetailsCrud() {
    const [companyId, setCompanyId] = useState("");
    const [appId, setAppId] = useState("");
    const [ddCosts, setCosts] = useState("");
    const [ddTime, setTime] = useState("");
    const [developmentDetails, setDevelopmentDetails] = useState([]);
    const [skip, setSkip] = useState(0);
    const [take, setTake] = useState(12);
    const [currentPage, setCurrentPage] = useState(1);
    const [ddCount, setCount] = useState(0);

    const config = {
        headers: { 'Authorization': 'Bearer ' + currentToken }
    };

    const totalPages = async () => {
        await axios.get("/api/DevelopmentDetails").data.length
    };

    const handleSelect = (e) => {

        console.log(e);

        setTake(e)

    }

    const useSortableData = (items, config = null) => {
        const [sortConfig, setSortConfig] = useState(config);

        const sortedItems = useMemo(() => {
            let sortableItems = [...items];
            if (sortConfig !== null) {
                sortableItems.sort((a, b) => {
                    if (a[sortConfig.key] < b[sortConfig.key]) {
                        return sortConfig.direction === 'ascending' ? -1 : 1;
                    }
                    if (a[sortConfig.key] > b[sortConfig.key]) {
                        return sortConfig.direction === 'ascending' ? 1 : -1;
                    }
                    return 0;
                });
            }
            return sortableItems;
        }, [items, sortConfig]);

        const requestSort = (key) => {
            let direction = 'ascending';
            if (
                sortConfig &&
                sortConfig.key === key &&
                sortConfig.direction === 'ascending'
            ) {
                direction = 'descending';
            }
            setSortConfig({ key, direction });
        };

        return { items: sortedItems, requestSort, sortConfig };
    };

    useEffect(() => {
        (async () => await Load())();
    }, []);

    useEffect(() => {
        Load();
    }, [skip, take]);

    useEffect(() => {
        setSkip((currentPage - 1) * take);
    }, [currentPage]);

    async function Load() {

        const result = await axios.get("/api/DevelopmentDetails/paginated/" + skip + "/" + take);
        setDevelopmentDetails(result.data);
        console.log(result.data);
        const resultCount = await axios.get("/api/DevelopmentDetails");
        setCount(resultCount.data.length);
    }

    async function save(event) {

        event.preventDefault();
        try {
            await axios.post("/api/DevelopmentDetails", {

                CompanyId: companyId,
                AppId: appId,
                DevelopmentCosts: ddCosts,
                DevelopmentTimeInHours: ddTime,

            }, config);
            alert("DevelopmentDetail Registation Successfully");
            setCompanyId("");
            setAppId("");
            setCosts("");
            setTime("");
            Load();
        } catch (err) {
            toast(err.message);
            //alert(err);
        }
    }

    async function editDevelopmentDetail(developmentDetails) {
        setCompanyId(developmentDetails.companyId);
        setAppId(developmentDetails.appId);
        setCosts(developmentDetails.developmentCosts);
        setTime(developmentDetails.developmetTimeInHours);
    }

    async function DeleteDevelopmentDetail(companyId, appId) {
        await axios.delete("/api/DevelopmentDetails/" + companyId + "/" + appId);
        alert("DevelopmentDetail deleted Successfully");
        setCompanyId("");
        setAppId("");
        setCosts("");
        setTime("");
        Load();
    }

    async function update(event) {
        event.preventDefault();
        try {

            await axios.put("/api/DevelopmentDetails/" + companyId + "/" + appId,
                {
                    CompanyId: companyId,
                    AppId: appId,
                    DevelopmentCosts: ddCosts,
                    DevelopmentTimeInHours: ddTime,

                }, config
            );
            alert("DevelopmentDetail Updated");
            setCompanyId("");
            setAppId("");
            setCosts("");
            setTime("");

            Load();
        } catch (err) {
            alert(err);
        }
    }

    const { items, requestSort, sortConfig } = useSortableData(developmentDetails);
    const getClassNamesFor = (name) => {
        if (!sortConfig) {
            return;
        }
        return sortConfig.key === name ? sortConfig.direction : undefined;
    };

    return (
        <div>
            <h1>Development Details</h1>
            <div class="container mt-4">
                <form>
                    <div class="form-group">

                        <label>DevelopmentDetail Company Id</label>
                        <input
                            type="text"
                            class="form-control"
                            id="CompanyId"
                            value={companyId}
                            onChange={(event) => {
                                setCompanyId(event.target.value);
                            }}
                        />

                    </div>
                    <div class="form-group">
                        <label>DevelopmentDetail App Id</label>
                        <input
                            type="text"
                            class="form-control"
                            id="AppId"
                            value={appId}
                            onChange={(event) => {
                                setAppId(event.target.value);
                            }}
                        />
                    </div>
                    <div class="form-group">
                        <label>Development Costs</label>
                        <input
                            type="text"
                            class="form-control"
                            id="ddCosts"
                            value={ddCosts}
                            onChange={(event) => {
                                setCosts(event.target.value);
                            }}
                        />
                    </div>
                    <div class="form-group">
                        <label>Development Time in Hours</label>
                        <input
                            type="text"
                            class="form-control"
                            id="ddTime"
                            value={ddTime}
                            onChange={(event) => {
                                setTime(event.target.value);
                            }}
                        />
                    </div>
                    <div>
                        <button class="btn btn-primary mt-4" onClick={save}>
                            Register
                        </button>
                        <button class="btn btn-warning mt-4" onClick={update}>
                            Update
                        </button>
                    </div>
                </form>
            </div>
            <br></br>


            <ToastContainer />
        </div>
    );
}

export default DevelopmentDetailsCrud;