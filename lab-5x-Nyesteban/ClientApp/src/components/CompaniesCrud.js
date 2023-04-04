import axios from "axios";
import { useEffect, useState } from "react";

function CompaniesCrud() {
    const [id, setId] = useState("");
    const [companyName, setName] = useState("");
    const [companyDescription, setDescription] = useState("");
    const [companyRevenue, setRevenue] = useState("");
    const [companyEstablishmentYear, setYear] = useState("");
    const [companyRating, setRating] = useState("");
    const [companies, setCompanies] = useState([]);
    const [companiesGames, setCompaniesGames] = useState([]);
    useEffect(() => {
        (async () => await Load())();
    }, []);
    async function Load() {

        const result = await axios.get("https://localhost:44454/api/Companies");
        setCompanies(result.data);
        console.log(result.data);
        const resultGames = await axios.get("https://localhost:44454/api/Companies/GameRating");
        setCompaniesGames(resultGames.data);
        console.log(resultGames.data);
    }
    async function save(event) {

        event.preventDefault();
        try {
            await axios.post("https://localhost:44454/api/Companies", {

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
            alert(err);
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
        await axios.delete("https://localhost:44454/api/Companies/" + id);
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

            await axios.put("https://localhost:44454/api/Companies/" + companies.find((u) => u.id === id).id || id,
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
                            }}
                        />
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
                        <th scope="col">Company Id</th>
                        <th scope="col">Company Name</th>
                        <th scope="col">Company Description</th>
                        <th scope="col">Company Revenue</th>
                        <th scope="col">Company Establishment Year</th>
                        <th scope="col">Company Rating</th>

                        <th scope="col">Option</th>
                    </tr>
                </thead>
                {companies.map(function fn(company) {
                    return (
                        <tbody>
                            <tr>
                                <th scope="row">{company.id} </th>
                                <td>{company.companyName}</td>
                                <td>{company.companyDescription}</td>
                                <td>{company.companyRevenue}</td>
                                <td>{company.companyEstablishmentYear}</td>
                                <td>{company.companyRating}</td>

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

        </div>
    );
}

export default CompaniesCrud;