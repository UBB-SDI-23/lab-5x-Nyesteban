import axios from "axios";
import { useEffect, useState, useMemo } from "react";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import Pagination from './Pagination';
import Dropdown from 'react-bootstrap/Dropdown';
import DropdownButton from 'react-bootstrap/DropdownButton';

function CompaniesCrud() {
    const [id, setId] = useState("");
    const [companyName, setName] = useState("");
    const [companyDescription, setDescription] = useState("");
    const [companyRevenue, setRevenue] = useState("");
    const [companyEstablishmentYear, setYear] = useState("");
    const [companyRating, setRating] = useState("");
    const [companies, setCompanies] = useState([]);
    const [companiesGames, setCompaniesGames] = useState([]);
    const [companiesApps, setCompaniesApps] = useState([]);
    const [skip, setSkip] = useState(0);
    const [take, setTake] = useState(12);
    const [skipGames, setSkipGames] = useState(0);
    const takeGames = 5;
    const [companiesMatch, setMatch] = useState([]);
    const [currentPage, setCurrentPage] = useState(1);
    const [companiesCount, setCount] = useState(0);
    const [companyIDknown, setIDknown] = useState(0);
    const [gameID, setGameID] = useState(0);

    const totalPages = async () => {
        await axios.get("/api/Companies").data.length
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
        LoadMainOnly();
    }, [skip, take]);

    useEffect(() => { 
        setSkip((currentPage-1)*take);
    }, [currentPage]);

    useEffect(() => {
        LoadGamesOnly();
    }, [skipGames]);

    const searchCompanies = (text) => {
        if (!text) {
            setMatch([]);
        }
        else {
            let matches = companies.filter((company) => {
                const regex = new RegExp(text, "gi");
                return company.companyName.match(regex);
            });
            setMatch(matches);
        }
    };


    async function Load() {

        const result = await axios.get("https://lab-5x-Nyesteban.chickenkiller.com/api/Companies/paginated/" + skip + "/" + take);
        setCompanies(result.data);
        console.log(result.data);
        const resultCount = await axios.get("https://lab-5x-Nyesteban.chickenkiller.com/api/Companies");
        setCount(resultCount.data.length);
        const resultGames = await axios.get("https://lab-5x-Nyesteban.chickenkiller.com/api/Companies/paginatedGameRating/" + skipGames + "/" + takeGames);
        setCompaniesGames(resultGames.data);
        console.log(resultGames.data);
        const resultApps = await axios.get("https://lab-5x-Nyesteban.chickenkiller.com/api/Companies/AppCount");
        setCompaniesApps(resultApps.data);
        console.log(resultApps.data);
    }
    async function LoadMainOnly() {

        const result = await axios.get("https://lab-5x-Nyesteban.chickenkiller.com/api/Companies/paginated/" + skip + "/" + take);
        setCompanies(result.data);
        console.log(result.data);
    }
    async function LoadGamesOnly() {

        const resultGames = await axios.get("https://lab-5x-Nyesteban.chickenkiller.com/api/Companies/paginatedGameRating/" + skipGames + "/" + takeGames);
        setCompaniesGames(resultGames.data);
        console.log(resultGames.data);
    }
    async function save(event) {

        event.preventDefault();
        if (companyRating < 0 || companyRating > 5) {
            toast("The company's rating must be between 0 and 5!");
            return;
        }
        try {
            await axios.post("https://lab-5x-Nyesteban.chickenkiller.com/api/Companies", {

                CompanyName: companyName,
                CompanyDescription: companyDescription,
                CompanyRevenue: companyRevenue,
                CompanyEstablishmentYear: companyEstablishmentYear,
                CompanyRating: companyRating

            });
            alert("Company Registation Successfully");
            setId("");
            setName("");
            setDescription("");
            setRevenue("");
            setYear("");
            setRating("");
            Load();
        } catch (err) {
            toast(err.message);
            //alert(err);
        }
    }

    async function gameIDAssigner(event) {

        event.preventDefault();
        try {
            await axios.put("https://lab-5x-Nyesteban.chickenkiller.com/api/Companies/" + companyIDknown + "/game/" + gameID);
            alert("Assignment Successful");
            setIDknown(0);
            setGameID(0);
            Load();
        } catch (err) {
            toast(err.message);
            //alert(err);
        }
    }

    async function editCompany(companies) {
        setName(companies.companyName);
        setDescription(companies.companyDescription);
        setRevenue(companies.companyRevenue);
        setYear(companies.companyEstablishmentYear);
        setRating(companies.companyRating);

        setId(companies.id);
    }

    async function DeleteCompany(id) {
        await axios.delete("https://lab-5x-Nyesteban.chickenkiller.com/api/Companies/" + id);
        alert("Company deleted Successfully");
        setId("");
        setName("");
        setDescription("");
        setRevenue("");
        setYear("");
        setRating("");
        Load();
    }
    
    async function update(event) {
        event.preventDefault();
        try {

            await axios.put("https://lab-5x-Nyesteban.chickenkiller.com/api/Companies/" + companies.find((u) => u.id === id).id || id,
                {
                    ID: id,
                    CompanyName: companyName,
                    CompanyDescription: companyDescription,
                    CompanyRevenue: companyRevenue,
                    CompanyEstablishmentYear: companyEstablishmentYear,
                    CompanyRating: companyRating

                }
            );
            alert("Registation Updated");
            setId("");
            setName("");
            setDescription("");
            setRevenue("");
            setYear("");
            setRating("");

            Load();
        } catch (err) {
            alert(err);
        }
    }

    const { items, requestSort, sortConfig } = useSortableData(companies);
    const getClassNamesFor = (name) => {
        if (!sortConfig) {
            return;
        }
        return sortConfig.key === name ? sortConfig.direction : undefined;
    };

    return (
        <div>
            <h1>Company Details</h1>
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

                        <label>Company Name</label>
                        <input
                            type="text"
                            class="form-control"
                            id="companyName"
                            value={companyName}
                            onChange={(event) => {
                                setName(event.target.value);
                                searchCompanies(event.target.value);
                            }}
                        />
                        {companiesMatch && companiesMatch.map((item,index) => (
                            <div key={index}>
                                <div class="card">
                                    <div class="card-body"
                                        onClick={() => setName(item.companyName)}
                                    >
                                        {item.companyName}
                                    </div>
                                </div>
                            </div>
                        ))}
                    </div>
                    <div class="form-group">
                        <label>Company Description</label>
                        <input
                            type="text"
                            class="form-control"
                            id="companyDescription"
                            value={companyDescription}
                            onChange={(event) => {
                                setDescription(event.target.value);
                            }}
                        />
                    </div>
                    <div class="form-group">
                        <label>Company Revenue</label>
                        <input
                            type="text"
                            class="form-control"
                            id="companyRevenue"
                            value={companyRevenue}
                            onChange={(event) => {
                                setRevenue(event.target.value);
                            }}
                        />
                    </div>
                    <div class="form-group">
                        <label>Company Establishment Year</label>
                        <input
                            type="text"
                            class="form-control"
                            id="companyEstablishmentYear"
                            value={companyEstablishmentYear}
                            onChange={(event) => {
                                setYear(event.target.value);
                            }}
                        />
                    </div>
                    <div class="form-group">
                        <label>Company Rating</label>
                        <input
                            type="text"
                            class="form-control"
                            id="companyRating"
                            value={companyRating}
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
                                Company Id
                            </button>
                        </th>
                        <th scope="col">
                            <button
                            type="button"
                            onClick={() => requestSort('companyName')}
                            className={getClassNamesFor('companyName')}
                        >
                            Company Name
                        </button>
                        </th>
                        <th scope="col">
                            <button
                                type="button"
                                onClick={() => requestSort('companyDescription')}
                                className={getClassNamesFor('companyDescription')}
                            >
                                Company Description
                            </button>
                        </th>
                        <th scope="col">
                            <button
                                type="button"
                                onClick={() => requestSort('companyRevenue')}
                                className={getClassNamesFor('companyRevenue')}
                            >
                                Company Revenue
                            </button>
                        </th>
                        <th scope="col">
                            <button
                                type="button"
                                onClick={() => requestSort('companyEstablishmentYear')}
                                className={getClassNamesFor('companyEstablishmentYear')}
                            >
                                Company Establishment Year
                            </button>
                        </th>
                        <th scope="col">
                            <button
                                type="button"
                                onClick={() => requestSort('companyRating')}
                                className={getClassNamesFor('companyRating')}
                            >
                                Company Rating
                            </button>
                        </th>

                        <th scope="col">
                             Company Games Count
                        </th>

                        <th scope="col">Option</th>
                    </tr>
                </thead>
                {items.map(function fn(company) {
                    return (
                        <tbody>
                            <tr>
                                <th scope="row">{company.id} </th>
                                <td>{company.companyName}</td>
                                <td>{company.companyDescription}</td>
                                <td>{company.companyRevenue}</td>
                                <td>{company.companyEstablishmentYear}</td>
                                <td>{company.companyRating}</td>
                                <td>{company.games.length}</td>

                                <td>
                                    <button
                                        type="button"
                                        class="btn btn-warning"
                                        onClick={() => editCompany(company)}
                                    >
                                        Edit
                                    </button>
                                    <button
                                        type="button"
                                        class="btn btn-danger"
                                        onClick={() => DeleteCompany(company.id)}
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
                totalCount={1000000}
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
                <label>Company ID</label>
                <input
                    type="text"
                    class="form-control"
                    id="companyID"
                    value={companyIDknown}
                    onChange={(event) => {
                        setIDknown(event.target.value);
                    }}
                />
            </div>
            <div class="form-group">
                <label>Game ID</label>
                <input
                    type="text"
                    class="form-control"
                    id="gameID"
                    value={gameID}
                    onChange={(event) => {
                        setGameID(event.target.value);
                    }}
                />
            </div>
            <div>
                <button class="btn btn-primary mt-4" onClick={gameIDAssigner}>
                    Assign Game ID to Company ID
                </button>
            </div>

            <br></br>

            <table class="table table-dark" align="center">
                <thead>
                    <tr>
                        <th scope="col">Company Name</th>
                        <th scope="col">Company Description</th>
                        <th scope="col">Company Revenue</th>
                        <th scope="col">Company Establishment Year</th>
                        <th scope="col">Company Rating</th>
                        <th scope="col">Average Game Rating</th>
                    </tr>
                </thead>
                {companiesGames.map(function fn(company) {
                    return (
                        <tbody>
                            <tr>
                                <th scope="row">{company.companyName}</th>
                                <td>{company.companyDescription}</td>
                                <td>{company.companyRevenue}</td>
                                <td>{company.companyEstablishmentYear}</td>
                                <td>{company.companyRating}</td>
                                <td>{company.averageGameRating}</td>
                            </tr>
                        </tbody>
                    );
                })}
            </table>

            <nav aria-label="Page navigation">
                <ul class="pagination">
                    <li class="page-item"><a class="page-link"
                        onClick={() => {
                            if (skipGames - takeGames >= 0) {
                                setSkipGames(skipGames - takeGames);
                            }
                        }}
                    >Previous</a></li>
                    <li class="page-item"><a class="page-link"
                        onClick={() => {
                            if (companiesGames.length >= take) {
                                setSkipGames(skipGames + takeGames);
                            }
                        }}
                    >Next</a></li>
                </ul>
            </nav>

            <br></br>

            <table class="table table-dark" align="center">
                <thead>
                    <tr>
                        <th scope="col">Company Name</th>
                        <th scope="col">Company Description</th>
                        <th scope="col">Company Revenue</th>
                        <th scope="col">Company Establishment Year</th>
                        <th scope="col">Company Rating</th>
                        <th scope="col">Company App Count</th>
                    </tr>
                </thead>
                {companiesApps.map(function fn(company) {
                    return (
                        <tbody>
                            <tr>
                                <th scope="row">{company.companyName}</th>
                                <td>{company.companyDescription}</td>
                                <td>{company.companyRevenue}</td>
                                <td>{company.companyEstablishmentYear}</td>
                                <td>{company.companyRating}</td>
                                <td>{company.companyAppCount}</td>
                            </tr>
                        </tbody>
                    );
                })}
            </table>

            <ToastContainer />
        </div>
    );
}

export default CompaniesCrud;