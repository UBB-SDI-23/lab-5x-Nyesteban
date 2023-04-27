import axios from "axios";
import { useEffect, useState, useMemo } from "react";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import Pagination from './Pagination';
import Dropdown from 'react-bootstrap/Dropdown';
import DropdownButton from 'react-bootstrap/DropdownButton';

function AppsCrud() {
    const [id, setId] = useState("");
    const [appName, setName] = useState("");
    const [appDescription, setDescription] = useState("");
    const [appVersion, setVersion] = useState("");
    const [appSize, setSize] = useState("");
    const [appPrice, setPrice] = useState("");
    const [appRating, setRating] = useState("");
    const [apps, setApps] = useState([]);
    const [skip, setSkip] = useState(0);
    const [take, setTake] = useState(12);
    const [currentPage, setCurrentPage] = useState(1);
    const [appsCount, setCount] = useState(0);
    const [appFilter, setFilter] = useState("");
    const [filteredApps, setFilteredApps] = useState([]);

    const totalPages = async () => {
        await axios.get("/api/Apps").data.length
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
        LoadFilter();
    }, [appFilter]);

    useEffect(() => {
        setSkip((currentPage - 1) * take);
    }, [currentPage]);

    async function Load() {

        const result = await axios.get("https://lab-5x-Nyesteban.chickenkiller.com/api/Apps/paginated/" + skip + "/" + take);
        setApps(result.data);
        console.log(result.data);
        const resultCount = await axios.get("https://lab-5x-Nyesteban.chickenkiller.com/api/Apps");
        setCount(resultCount.data.length);
    }

    async function LoadFilter() {
        if (isNaN(+appFilter))
            return;
        const result = await axios.get("https://lab-5x-Nyesteban.chickenkiller.com/api/Apps/" + appFilter +"/filter");
        setFilteredApps(result.data);
        console.log(result.data);
    }

    async function save(event) {

        event.preventDefault();
        if (appSize < 0) {
            toast("The app's size must be greater than 0!");
            return;
        }
        if (appPrice < 0) {
            toast("The app's price must be greater than 0!");
            return;
        }
        try {
            await axios.post("https://lab-5x-Nyesteban.chickenkiller.com/api/Apps", {

                AppName: appName,
                AppDescription: appDescription,
                AppVersion: appVersion,
                AppSize: appSize,
                AppPrice: appPrice,
                AppRating: appRating

            });
            alert("App Registation Successfully");
            setId("");
            setName("");
            setDescription("");
            setSize("");
            setVersion("");
            setPrice("");
            setRating("");
            Load();
        } catch (err) {
            toast(err.message);
            //alert(err);
        }
    }

    async function editApp(apps) {
        setName(apps.appName);
        setDescription(apps.appDescription);
        setSize(apps.appSize);
        setVersion(apps.appVersion);
        setPrice(apps.appPrice);
        setRating(apps.appRating);

        setId(apps.id);
    }

    async function DeleteApp(id) {
        await axios.delete("https://lab-5x-Nyesteban.chickenkiller.com/api/Apps/" + id);
        alert("App deleted Successfully");
        setId("");
        setName("");
        setDescription("");
        setSize("");
        setVersion("");
        setPrice("");
        setRating("");
        Load();
    }

    async function update(event) {
        event.preventDefault();
        try {

            await axios.put("https://lab-5x-Nyesteban.chickenkiller.com/api/Apps/" + apps.find((u) => u.id === id).id || id,
                {
                    ID: id,
                    AppName: appName,
                    AppDescription: appDescription,
                    AppVersion: appVersion,
                    AppSize: appSize,
                    AppPrice: appPrice,
                    AppRating: appRating

                }
            );
            alert("App Updated");
            setId("");
            setName("");
            setDescription("");
            setSize("");
            setVersion("");
            setPrice("");
            setRating("");

            Load();
        } catch (err) {
            alert(err);
        }
    }

    const { items, requestSort, sortConfig } = useSortableData(apps);
    const getClassNamesFor = (name) => {
        if (!sortConfig) {
            return;
        }
        return sortConfig.key === name ? sortConfig.direction : undefined;
    };

    return (
        <div>
            <h1>App Details</h1>
            <div class="container mt-4">
                <form>
                    <div class="form-group">

                        <input
                            type="text"
                            class="form-control"
                            id="id"
                            hidden
                            value={id}
                            onChange={(event) => {
                                setId(event.target.value);
                            }}
                        />

                        <label>App Name</label>
                        <input
                            type="text"
                            class="form-control"
                            id="AppName"
                            value={appName}
                            onChange={(event) => {
                                setName(event.target.value);
                            }}
                        />

                    </div>
                    <div class="form-group">
                        <label>App Description</label>
                        <input
                            type="text"
                            class="form-control"
                            id="AppDescription"
                            value={appDescription}
                            onChange={(event) => {
                                setDescription(event.target.value);
                            }}
                        />
                    </div>
                    <div class="form-group">
                        <label>App Version</label>
                        <input
                            type="text"
                            class="form-control"
                            id="appVersion"
                            value={appVersion}
                            onChange={(event) => {
                                setVersion(event.target.value);
                            }}
                        />
                    </div>
                    <div class="form-group">
                        <label>App Size</label>
                        <input
                            type="text"
                            class="form-control"
                            id="appSize"
                            value={appSize}
                            onChange={(event) => {
                                setSize(event.target.value);
                            }}
                        />
                    </div>
                    <div class="form-group">
                        <label>App Price</label>
                        <input
                            type="text"
                            class="form-control"
                            id="appPrice"
                            value={appPrice}
                            onChange={(event) => {
                                setPrice(event.target.value);
                            }}
                        />
                    </div>
                    <div class="form-group">
                        <label>App Rating</label>
                        <input
                            type="text"
                            class="form-control"
                            id="appRating"
                            value={appRating}
                            onChange={(event) => {
                                setRating(event.target.value);
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

            <table class="table table-dark" align="center">
                <thead>
                    <tr>
                        <th scope="col">
                            <button
                                type="button"
                                onClick={() => requestSort('id')}
                                className={getClassNamesFor('id')}
                            >
                                App Id
                            </button>
                        </th>
                        <th scope="col">
                            <button
                                type="button"
                                onClick={() => requestSort('appName')}
                                className={getClassNamesFor('appName')}
                            >
                                App Name
                            </button>
                        </th>
                        <th scope="col">
                            <button
                                type="button"
                                onClick={() => requestSort('appDescription')}
                                className={getClassNamesFor('appDescription')}
                            >
                                App Description
                            </button>
                        </th>
                        <th scope="col">
                            <button
                                type="button"
                                onClick={() => requestSort('appVersion')}
                                className={getClassNamesFor('appVersion')}
                            >
                                App Version
                            </button>
                        </th>
                        <th scope="col">
                            <button
                                type="button"
                                onClick={() => requestSort('appSize')}
                                className={getClassNamesFor('appSize')}
                            >
                                App Size
                            </button>
                        </th>
                        <th scope="col">
                            <button
                                type="button"
                                onClick={() => requestSort('appPrice')}
                                className={getClassNamesFor('appPrice')}
                            >
                                App Size
                            </button>
                        </th>
                        <th scope="col">
                            <button
                                type="button"
                                onClick={() => requestSort('appRating')}
                                className={getClassNamesFor('appRating')}
                            >
                                App Rating
                            </button>
                        </th>

                        <th scope="col">
                            App Companies Count
                        </th>

                        <th scope="col">Option</th>
                    </tr>
                </thead>
                {items.map(function fn(app) {
                    return (
                        <tbody>
                            <tr>
                                <th scope="row">{app.id} </th>
                                <td>{app.appName}</td>
                                <td>{app.appDescription}</td>
                                <td>{app.appVersion}</td>
                                <td>{app.appSize}</td>
                                <td>{app.appPrice}</td>
                                <td>{app.appRating}</td>
                                <td>{app.developmentDetails.length}</td>

                                <td>
                                    <button
                                        type="button"
                                        class="btn btn-warning"
                                        onClick={() => editApp(app)}
                                    >
                                        Edit
                                    </button>
                                    <button
                                        type="button"
                                        class="btn btn-danger"
                                        onClick={() => DeleteApp(app.id)}
                                    >
                                        Delete
                                    </button>
                                </td>
                            </tr>
                        </tbody>
                    );
                })}
            </table>

            <Pagination
                className="pagination-bar"
                currentPage={currentPage}
                totalCount={appsCount}
                pageSize={take}
                onPageChange={page => setCurrentPage(page)}
            />

            <br></br>

            <DropdownButton

                alignRight

                title="Select no of pages"

                id="dropdown-menu-align-right"

                onSelect={handleSelect}

            >

                <Dropdown.Item eventKey={12}>12</Dropdown.Item>

                <Dropdown.Item eventKey={20}>20</Dropdown.Item>

                <Dropdown.Item eventKey={40}>40</Dropdown.Item>

            </DropdownButton>

            <br></br>

            <div class="form-group">
                <label>Filter by size</label>
                <input
                    type="text"
                    class="form-control"
                    id="appFilter"
                    value={appFilter}
                    onChange={(event) => {
                        setFilter(event.target.value);
                    }}
                />
            </div>

            <table class="table table-dark" align="center">
                <thead>
                    <tr>
                        <th scope="col">App Name</th>
                        <th scope="col">App Description</th>
                        <th scope="col">App Version</th>
                        <th scope="col">App Size</th>
                        <th scope="col">App Price</th>
                        <th scope="col">App Rating</th>
                    </tr>
                </thead>
                {filteredApps.map(function fn(app) {
                    return (
                        <tbody>
                            <tr>
                                <th scope="row">{app.appName}</th>
                                <td>{app.appDescription}</td>
                                <td>{app.appVersion}</td>
                                <td>{app.appSize}</td>
                                <td>{app.appPrice}</td>
                                <td>{app.appRating}</td>
                            </tr>
                        </tbody>
                    );
                })}
            </table>

            <ToastContainer />
        </div>
    );
}

export default AppsCrud;