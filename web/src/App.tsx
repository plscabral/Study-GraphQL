import { gql, useQuery, useMutation } from "@apollo/client";
import { FormEvent, useEffect, useState } from "react";
import { Trash } from 'phosphor-react';

interface GetUsersQueryResponse {
  users: {
    id: string,
    name: string,
    email: string
  }[]
}

const GET_USERS_QUERY = gql`
  query {
    users {
      id,
      name,
      email
    }
  }
`;

const INSERT_USER_MUTATION = gql`
  mutation InsertUser($name: String!, $email: String!, $password: String!){
    insertUser(user: { name: $name, email: $email, password: $password}) {
      id
    }
  }
`;

const DELETE_USER_MUTATION = gql`
  mutation DeleteUser($id: String!){
    deleteUser(id: $id)
  }
`;

export function App() {
  const [name, setName] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  // const [users, setUsers] = useState<GetUsersQueryResponse>();

  const [insertUser, { loading }] = useMutation(INSERT_USER_MUTATION);
  const [deleteUser] = useMutation(DELETE_USER_MUTATION);
  const responseGetUsers = useQuery<GetUsersQueryResponse>(GET_USERS_QUERY);

  // useEffect(() => {
  //   setUsers(responseGetUsers.data)
  // }, [loading])

  function Clear() {
    setName('');
    setEmail('');
    setPassword('');
  }

  async function handleSubmit(event: FormEvent) {
    event.preventDefault();

    await insertUser({
      variables: {
        name,
        email,
        password
      }
    });

    Clear();
    alert('Usuário cadastrado com sucesso!')
  }

  async function handleDeleteUser(id: string) {
    await deleteUser({
      variables: {
        id
      }
    });

    alert('Usuário deletado com sucesso!')
  }

  return (
    <div className="flex flex-col items-center justify-center p-8 h-full w-full">
      <h1 className="text-2xl">Inserting User</h1>

      <form className="flex flex-col gap-4 mt-4 w-[50vw]" onSubmit={handleSubmit}>
        <input
          className="rounded p-2 text-black"
          type="text"
          placeholder="Name"
          value={name}
          onChange={e => setName(e.target.value)}
        />

        <input
          className="rounded p-2 text-black"
          type="text"
          placeholder="E-mail"
          value={email}
          onChange={e => setEmail(e.target.value)}
        />

        <input
          className="rounded p-2 text-black"
          type="password"
          placeholder="Password"
          value={password}
          onChange={e => setPassword(e.target.value)}
        />

        <div className="flex flex-1 justify-end gap-4">
          <button className="bg-red-500 rounded p-2 w-[200px] uppercase" onClick={Clear} type="button">
            Clear
          </button>

          <button className="bg-green-500 rounded p-2 w-[200px] uppercase" type="submit">
            Insert
          </button>
        </div>
      </form>

      <div className="m-8 border-t py-10 border-gray-500 w-[50vw]">
        <table className="border border-gray-500 w-full">
          <thead>
            <tr>
              <th className="border border-gray-500"></th>
              <th className="border border-gray-500">Id</th>
              <th className="border border-gray-500">Name</th>
              <th className="border border-gray-500">E-mail</th>
            </tr>
          </thead>
          <tbody>
            {
              responseGetUsers.data?.users?.map(user => {
                return (
                  <tr key={user.id}>
                    <td className="flex justify-center p-1 border border-gray-500">
                      <button onClick={e => handleDeleteUser(user.id)}>
                        <Trash size={20} className="text-red-600" />
                      </button>
                    </td>
                    <td className="text-center border border-gray-500">{user.id}</td>
                    <td className="text-center border border-gray-500">{user.name}</td>
                    <td className="text-center border border-gray-500">{user.email}</td>
                  </tr>
                )
              })
            }
          </tbody>
        </table>
      </div>
    </div>
  );
}
