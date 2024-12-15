import React, { useState, useEffect } from "react";
import { Button, Form, Alert, Modal } from "react-bootstrap";
import { api } from "../api";

const AddPlayerPage = () => {
  const [name, setName] = useState("");
  const [surname, setSurname] = useState("");
  const [sex, setSex] = useState(0);
  const [birthDate, setBirthDate] = useState("");
  const [commandTitle, setCommandTitle] = useState("");
  const [countryId, setCountryId] = useState(1);
  const [countries] = useState([
    { id: 1, title: "Россия" },
    { id: 2, title: "США" },
    { id: 3, title: "Италия" },
  ]);
  const [commands, setCommands] = useState([]);
  const [showSuccess, setShowSuccess] = useState(false);
  const [showError, setShowError] = useState(false);
  const [errorMessage, setErrorMessage] = useState("");
  const [isSaving, setIsSaving] = useState(false);

  useEffect(() => {
    const fetchCommands = async () => {
      try {
        const response = await api.get("/commands");
        setCommands(response.data);
      } catch (error) {
        console.error("Ошибка при загрузке команд:", error);
        setShowError(true);
        setErrorMessage("Не удалось загрузить список команд.");
      }
    };

    fetchCommands();
  }, []);

  const handleAddPlayer = async () => {
    const newPlayer = {
      name,
      surname,
      sex,
      birthDate,
      commandTitle,
      countryId,
    };

    try {
      setIsSaving(true);
      await api.post("/players", newPlayer);
      setShowSuccess(true);
      setName("");
      setSurname("");
      setSex(0);
      setBirthDate("");
      setCommandTitle("");
      setCountryId(1); // Reset to default country
    } catch (error) {
      setShowError(true);
      setErrorMessage("Ошибка при добавлении игрока. Попробуйте снова.");
    } finally {
      setIsSaving(false);
    }
  };

  return (
    <div className="container mt-5">
      <h2 className="text-center mb-4">Добавить нового футболиста</h2>

      {/* Уведомления */}
      {showSuccess && (
        <Alert variant="success" onClose={() => setShowSuccess(false)} dismissible>
          Игрок успешно добавлен!
        </Alert>
      )}
      {showError && (
        <Alert variant="danger" onClose={() => setShowError(false)} dismissible>
          {errorMessage}
        </Alert>
      )}

      <Form>
        <Form.Group className="mb-3">
          <Form.Label>Имя</Form.Label>
          <Form.Control
            type="text"
            value={name}
            onChange={(e) => setName(e.target.value)}
            placeholder="Введите имя"
          />
        </Form.Group>

        <Form.Group className="mb-3">
          <Form.Label>Фамилия</Form.Label>
          <Form.Control
            type="text"
            value={surname}
            onChange={(e) => setSurname(e.target.value)}
            placeholder="Введите фамилию"
          />
        </Form.Group>

        <Form.Group className="mb-3">
          <Form.Label>Пол</Form.Label>
          <Form.Select value={sex} onChange={(e) => setSex(Number(e.target.value))}>
            <option value={0}>Мужской</option>
            <option value={1}>Женский</option>
          </Form.Select>
        </Form.Group>

        <Form.Group className="mb-3">
          <Form.Label>Дата рождения</Form.Label>
          <Form.Control
            type="date"
            value={birthDate}
            onChange={(e) => setBirthDate(e.target.value)}
          />
        </Form.Group>

        <Form.Group className="mb-3">
          <Form.Label>Команда</Form.Label>
          <Form.Select
            value={commandTitle}
            onChange={(e) => setCommandTitle(e.target.value)}
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
            value={commandTitle}
            onChange={(e) => setCommandTitle(e.target.value)}
            placeholder="Название команды"
          />
        </Form.Group>

        <Form.Group className="mb-3">
          <Form.Label>Страна</Form.Label>
          <Form.Select
            value={countryId}
            onChange={(e) => setCountryId(Number(e.target.value))}
          >
            {countries.map((country) => (
              <option key={country.id} value={country.id}>
                {country.title}
              </option>
            ))}
          </Form.Select>
        </Form.Group>

        <Button
          variant="primary"
          onClick={handleAddPlayer}
          disabled={isSaving}
        >
          {isSaving ? "Сохранение..." : "Добавить футболиста"}
        </Button>
      </Form>
    </div>
  );
};

export default AddPlayerPage;
