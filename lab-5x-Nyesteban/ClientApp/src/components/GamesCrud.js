﻿import axios from "axios";
import { useEffect, useState, useMemo } from "react";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import Pagination from './Pagination';
import Dropdown from 'react-bootstrap/Dropdown';
import DropdownButton from 'react-bootstrap/DropdownButton';
import { Link } from 'react-router-dom';
import { showCount } from './Profile';
import { currentToken, currentUserId } from "./Login";

function GamesCrud() {
    const [id, setId] = useState("");
    const [gameName, setName] = useState("");
    const [gameDescription, setDescription] = useState("");
    const [gameLength, setLength] = useState("");
    const [gameSize, setSize] = useState("");
    const [gameRating, setRating] = useState("");
    const [selectedUserId, setUserId] = useState(0);
    const [games, setGames] = useState([]);
    const [skip, setSkip] = useState(0);
    const [take, setTake] = useState(showCount);
    const [currentPage, setCurrentPage] = useState(1);
    const [gamesCount, setCount] = useState(0);

    const config = {
        headers: { 'Authorization': 'Bearer ' + currentToken }
    };

    const totalPages = async () => {
        await axios.get("https://nyesteban.twilightparadox.com/api/Games").data.length
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

        const result = await axios.get("https://nyesteban.twilightparadox.com/api/Games/paginated/" + skip + "/" + take);
        setGames(result.data);
        console.log(result.data);
        const resultCount = await axios.get("https://nyesteban.twilightparadox.com/api/Games");
        setCount(resultCount.data.length);
    }

    async function save(event) {

        event.preventDefault();
        if (gameRating < 0 || gameRating > 5) {
            toast("The game's rating must be between 0 and 5!");
            return;
        }
        if (gameSize < 0) {
            toast("The game's size must be greater than 0!");
            return;
        }
        if (gameLength < 0) {
            toast("The game's length must be greater than 0!");
            return;
        }
        try {
            await axios.post("https://nyesteban.twilightparadox.com/api/Games", {

                GameName: gameName,
                GameDescription: gameDescription,
                GameLength: gameLength,
                GameSize: gameSize,
                GameRating: gameRating,
                UserID: currentUserId

            }, config);
            alert("Game Registation Successfully");
            setId("");
            setName("");
            setDescription("");
            setSize("");
            setLength("");
            setRating("");
            Load();
        } catch (err) {
            toast(err.message);
            //alert(err);
        }
    }

    async function editGame(games) {
        setName(games.gameName);
        setDescription(games.gameDescription);
        setSize(games.gameSize);
        setLength(games.gameLength);
        setRating(games.gameRating);

        setId(games.id);
        setUserId(games.userID);
    }

    async function DeleteGame(games) {
        try {
            if (games.userID != currentUserId)
                await axios.delete("https://nyesteban.twilightparadox.com/api/Games/" + games.id, config);
            else
                await axios.delete("https://nyesteban.twilightparadox.com/api/Games/" + games.id + "/sameuser", config);
            alert("Game deleted Successfully");
            setId("");
            setName("");
            setDescription("");
            setSize("");
            setLength("");
            setRating("");
            Load();
        } catch (err) {
            toast(err.message);
            //alert(err);
        }
    }

    async function update(event) {
        event.preventDefault();
        try {
            if (selectedUserId != currentUserId) {
                await axios.put("https://nyesteban.twilightparadox.com/api/Games/" + games.find((u) => u.id === id).id || id,
                    {
                        ID: id,
                        GameName: gameName,
                        GameDescription: gameDescription,
                        GameLength: gameLength,
                        GameSize: gameSize,
                        GameRating: gameRating,
                        UserID: selectedUserId

                    }, config
                );
            }
            else {
                await axios.put("https://nyesteban.twilightparadox.com/api/Games/" + (games.find((u) => u.id === id).id || id) + "/sameuser",
                    {
                        ID: id,
                        GameName: gameName,
                        GameDescription: gameDescription,
                        GameLength: gameLength,
                        GameSize: gameSize,
                        GameRating: gameRating,
                        UserID: selectedUserId

                    }, config
                );
            }
            alert("Game Updated");
            setId("");
            setName("");
            setDescription("");
            setSize("");
            setLength("");
            setRating("");

            Load();
        } catch (err) {
            toast(err.message);
            //alert(err);
        }
    }

    const { items, requestSort, sortConfig } = useSortableData(games);
    const getClassNamesFor = (name) => {
        if (!sortConfig) {
            return;
        }
        return sortConfig.key === name ? sortConfig.direction : undefined;
    };

    return (
        <div>
            <h1>Game Details</h1>
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

                        <label>Game Name</label>
                        <input
                            type="text"
                            class="form-control"
                            id="GameName"
                            value={gameName}
                            onChange={(event) => {
                                setName(event.target.value);
                            }}
                        />
                        
                    </div>
                    <div class="form-group">
                        <label>Game Description</label>
                        <input
                            type="text"
                            class="form-control"
                            id="GameDescription"
                            value={gameDescription}
                            onChange={(event) => {
                                setDescription(event.target.value);
                            }}
                        />
                    </div>
                    <div class="form-group">
                        <label>Game Length</label>
                        <input
                            type="text"
                            class="form-control"
                            id="gameLength"
                            value={gameLength}
                            onChange={(event) => {
                                setLength(event.target.value);
                            }}
                        />
                    </div>
                    <div class="form-group">
                        <label>Game Size</label>
                        <input
                            type="text"
                            class="form-control"
                            id="gameSize"
                            value={gameSize}
                            onChange={(event) => {
                                setSize(event.target.value);
                            }}
                        />
                    </div>
                    <div class="form-group">
                        <label>Game Rating</label>
                        <input
                            type="text"
                            class="form-control"
                            id="gameRating"
                            value={gameRating}
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
                                Game Id
                            </button>
                        </th>
                        <th scope="col">
                            <button
                                type="button"
                                onClick={() => requestSort('gameName')}
                                className={getClassNamesFor('gameName')}
                            >
                                Game Name
                            </button>
                        </th>
                        <th scope="col" class="d-none d-sm-table-cell">
                            <button
                                type="button"
                                onClick={() => requestSort('gameDescription')}
                                className={getClassNamesFor('gameDescription')}
                            >
                                Game Description
                            </button>
                        </th>
                        <th scope="col" class="d-none d-sm-table-cell">
                            <button
                                type="button"
                                onClick={() => requestSort('gameLength')}
                                className={getClassNamesFor('gameLength')}
                            >
                                Game Length
                            </button>
                        </th>
                        <th scope="col" class="d-none d-sm-table-cell">
                            <button
                                type="button"
                                onClick={() => requestSort('gameSize')}
                                className={getClassNamesFor('gameSize')}
                            >
                                Game Size
                            </button>
                        </th>
                        <th scope="col">
                            <button
                                type="button"
                                onClick={() => requestSort('gameRating')}
                                className={getClassNamesFor('gameRating')}
                            >
                                Game Rating
                            </button>
                        </th>

                        <th scope="col">
                            User
                        </th>

                        <th scope="col">Option</th>
                    </tr>
                </thead>
                {items.map(function fn(game) {
                    return (
                        <tbody>
                            <tr>
                                <th scope="row">{game.id} </th>
                                <td>{game.gameName}</td>
                                <td class="d-none d-sm-table-cell">{game.gameDescription}</td>
                                <td class="d-none d-sm-table-cell">{game.gameLength}</td>
                                <td class="d-none d-sm-table-cell">{game.gameSize}</td>
                                <td>{game.gameRating}</td>
                                <td><Link to="/profile"  state={{info: game.user.id}} className="btn btn-primary">{game.user.username}</Link></td>

                                <td>
                                    <button
                                        type="button"
                                        class="btn btn-warning"
                                        onClick={() => editGame(game)}
                                    >
                                        Edit
                                    </button>
                                    <button
                                        type="button"
                                        class="btn btn-danger"
                                        onClick={() => DeleteGame(game)}
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

            <ToastContainer />
        </div>
    );
}

export default GamesCrud;