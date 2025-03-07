import api from "../axiosIntance";
import { Person } from "../types/Person";

const basePath = "/person";

const GetPeople = async (): Promise<Person[]> => {
  const response = await api.get(basePath);
  return response.data;
};

const PersonService = {
  GetPeople,
};

export default PersonService;
