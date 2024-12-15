import React, { useEffect, useState } from "react";
import { Modal, Button, Form, Alert } from "react-bootstrap";
import { HubConnectionBuilder } from "@microsoft/signalr";
import { api, hubUrl } from "../api";

const PlayerListPage = () => {
  const [connection, setConnection] = useState(null);
  const [players, setPlayers] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);
  const [successMessage, setSuccessMessage] = useState(null);
  const [actionError, setActionError] = useState(null);
  const [showEditModal, setShowEditModal] = useState(false);
  const [showDeleteModal, setShowDeleteModal] = useState(false);
  const [selectedPlayer, setSelectedPlayer] = useState(null);
const [commands, setCommands] = useState([]);

  const countries = [
    { id: 1, title: "Россия" },
    { id: 2, title: "США" },
    { id: 3, title: "Италия" },
  ];

  useEffect(() => {
    fetchPlayers();
    fetchConnect();
  }, []);

  const fetchPlayers = async () => {
    try {
      setLoading(true);
      const response = await api.get("/players");
      setPlayers(response.data);
    } catch (err) {
      console.error("Ошибка при загрузке списка игроков:", err);
      setError("Не удалось загрузить список игроков.");
    } finally {
      setLoading(false);
    }
  };

  const fetchConnect = async () => {
    const newConnection = new HubConnectionBuilder()
    .withUrl(hubUrl)
    .withAutomaticReconnect()
    .build();
    setConnection(newConnection);
  };

  useEffect(() => {
    if (connection) {
        connection.start()
            .then(() => {
                console.log("Connected to SignalR");

                connection.on("PlayerAdded", (newPlayer) => {
                    setPlayers((prevPlayers) => [...prevPlayers, newPlayer]);
                });

                connection.on("PlayerUpdated", (updatedPlayer) => {
                    setPlayers((prevPlayers) =>
                        prevPlayers.map((player) =>
                            player.id === updatedPlayer.id ? updatedPlayer : player
                        )
                    );
                });

                connection.on("PlayerDeleted", (deletedPlayerId) => {
                    setPlayers((prevPlayers) =>
                        prevPlayers.filter((player) => player.id !== deletedPlayerId)
                    );
                });
            })
            .catch((error) => console.error("Connection failed: ", error));
    }
}, [connection]);



  const handleEdit = async (playerId) => {
    const response = await api.get(`/players/${playerId}`, selectedPlayer);
    setSelectedPlayer(response.data);
      try {
        const response = await api.get("/commands");
        setCommands(response.data);
      } catch (error) {
        console.error("Ошибка при загрузке команд:", error);
        setError("Не удалось загрузить список команд.");
      }
    setShowEditModal(true);
  };

  const closeEditModal = () => {
    setShowEditModal(false);
    setSelectedPlayer(null);
  };

  const handleDelete = (player) => {
    setSelectedPlayer(player);
    setShowDeleteModal(true);
  };

  const closeDeleteModal = () => {
    setShowDeleteModal(false);
    setSelectedPlayer(null);
  };

  const handleSaveChanges = async () => {
    if (!selectedPlayer) return;

    const updateDto = {
      id: selectedPlayer.id,
      name: selectedPlayer.name,
      surname: selectedPlayer.surname,
      sex: selectedPlayer.sex,
      birthDate: selectedPlayer.birthDate,
      commandTitle: selectedPlayer.commandTitle,
      countryId: selectedPlayer.countryId,
    };

    try {
      await api.patch(`/players`, updateDto);
      setSuccessMessage("Данные игрока успешно обновлены!");
      fetchPlayers();
      closeEditModal();
    } catch (error) {
      console.error("Ошибка при обновлении данных игрока:", error);
      setActionError("Не удалось обновить данные игрока.");
    }
  };

  const handleConfirmDelete = async () => {
    if (!selectedPlayer) return;

    try {
      await api.delete(`/players/${selectedPlayer.id}`);
      setSuccessMessage("Игрок успешно удалён!");
      fetchPlayers();
      closeDeleteModal();
    } catch (error) {
      console.error("Ошибка при удалении игрока:", error);
      setActionError("Не удалось удалить игрока.");
    }
  };

  return (
    <div className="container mt-5">
      <h2 className="text-center mb-4">Список футболистов</h2>

      {loading && <p className="text-center">Загрузка...</p>}
      {error && <p className="text-danger text-center">{error}</p>}

      {successMessage && (
        <Alert
          variant="success"
          onClose={() => setSuccessMessage(null)}
          dismissible
        >
          {successMessage}
        </Alert>
      )}
      {actionError && (
        <Alert
          variant="danger"
          onClose={() => setActionError(null)}
          dismissible
        >
          {actionError}
        </Alert>
      )}

      {!loading && !error && players.length > 0 && (
        <table className="table table-striped">
          <thead>
            <tr>
              <th>#</th>
              <th>Имя</th>
              <th>Фамилия</th>
              <th>Пол</th>
              <th>Дата рождения</th>
              <th>Команда</th>
              <th>Страна</th>
              <th>Действия</th>
            </tr>
          </thead>
          <tbody>
            {players.map((player, index) => (
              <tr key={player.id}>
                <td>{index + 1}</td>
                <td>{player.name}</td>
                <td>{player.surname}</td>
                <td>{player.sex === 0 ? "Мужской" : "Женский"}</td>
                <td>{new Date(player.birthDate).toLocaleDateString()}</td>
                <td>{player.commandTitle || "Без команды"}</td>
                <td>{player.countryTitle}</td>
                <td>
                  <button
                    className="btn btn-primary btn-sm me-2"
                    onClick={() => handleEdit(player.id)}
                  >
                    Редактировать
                  </button>
                  <button
                    className="btn btn-danger btn-sm"
                    onClick={() => handleDelete(player)}
                  >
                    Удалить
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      )}

      {!loading && !error && players.length === 0 && (
        <p className="text-center">Список игроков пуст.</p>
      )}

      <Modal show={showEditModal} onHide={closeEditModal}>
        <Modal.Header closeButton>
          <Modal.Title>Редактировать игрока</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          {selectedPlayer && (
            <Form>
              <Form.Group className="mb-3">
                <Form.Label>Имя</Form.Label>
                <Form.Control
                  type="text"
                  value={selectedPlayer.name}
                  onChange={(e) =>
                    setSelectedPlayer({ ...selectedPlayer, name: e.target.value })
                  }
                />
              </Form.Group>
              <Form.Group className="mb-3">
                <Form.Label>Фамилия</Form.Label>
                <Form.Control
                  type="text"
                  value={selectedPlayer.surname}
                  onChange={(e) =>
                    setSelectedPlayer({ ...selectedPlayer, surname: e.target.value })
                  }
                />
              </Form.Group>
              <Form.Group className="mb-3">
                <Form.Label>Пол</Form.Label>
                <Form.Select
                  value={selectedPlayer.sex}
                  onChange={(e) =>
                    setSelectedPlayer({ ...selectedPlayer, sex: parseInt(e.target.value) })
                  }
                >
                  <option value="0">Мужской</option>
                  <option value="1">Женский</option>
                </Form.Select>
              </Form.Group>
              <Form.Group className="mb-3">
                <Form.Label>Дата рождения</Form.Label>
                <Form.Control
                  type="date"
                  value={new Date(selectedPlayer.birthDate).toISOString().split("T")[0]}
                  onChange={(e) =>
                    setSelectedPlayer({
                      ...selectedPlayer,
                      birthDate: e.target.value,
                    })
                  }
                />
              </Form.Group>

              <Form.Group className="mb-3">
                <Form.Label>Команда</Form.Label>
                <Form.Select
                  value={selectedPlayer.commandTitle}
                  onChange={(e) =>
                    setSelectedPlayer({
                      ...selectedPlayer,
                      commandTitle: e.target.value,
                    })
                  }
                >
                  <option value="">Выберите команду</option>
                  {commands.map((command) => (
                    <option key={command.id} value={command.title}>
                      {command.title}
                    </option>
                  ))}
                </Form.Select>
                <Form.Text className="text-muted">
                  Или введите название новой команды:
                </Form.Text>
                <Form.Control
                  type="text"
                  value={selectedPlayer.commandTitle}
                  onChange={(e) =>
                    setSelectedPlayer({
                      ...selectedPlayer,
                      commandTitle: e.target.value,
                    })
                  }
                  placeholder="Название команды"
                />
              </Form.Group>

              <Form.Group className="mb-3">
                <Form.Label>Страна</Form.Label>
                <Form.Select
                  value={selectedPlayer.countryId}
                  onChange={(e) =>
                    setSelectedPlayer({
                      ...selectedPlayer,
                      countryId: parseInt(e.target.value),
                    })
                  }
                >
                  {countries.map((country) => (
                    <option key={country.id} value={country.id}>
                      {country.title}
                    </option>
                  ))}
                </Form.Select>
              </Form.Group>
            </Form>
          )}
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={closeEditModal}>
            Отмена
          </Button>
          <Button variant="primary" onClick={handleSaveChanges}>
            Сохранить изменения
          </Button>
        </Modal.Footer>
      </Modal>

      <Modal show={showDeleteModal} onHide={closeDeleteModal}>
        <Modal.Header closeButton>
          <Modal.Title>Подтверждение удаления</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          Вы действительно хотите удалить игрока{" "}
          <strong>{selectedPlayer?.name} {selectedPlayer?.surname}</strong>?
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={closeDeleteModal}>
            Отмена
          </Button>
          <Button variant="danger" onClick={handleConfirmDelete}>
            Удалить
          </Button>
        </Modal.Footer>
      </Modal>
    </div>
  );
};

export default PlayerListPage;
