import { Button, Container } from "react-bootstrap";
import React, { Component, useEffect, useState } from "react";
import { Table } from "react-bootstrap";
import { User } from "../../Apis";
import { useGetUserList } from "../../Data/User/useGetUserList";
import { Link } from "react-router-dom";


export function UserList() {
  const [users, setUsers] = useState<User[]>([]);

  useEffect(() => {
    async function GetUsers() {
      setUsers(await useGetUserList());
    }
    GetUsers()
  }, [])

  return (
    <Container>
      <Table striped bordered>
        <thead>
          <tr>
            <th>Id</th>
            <th>Name</th>
            <th>Email</th>
            <th>Admin</th>
            <th></th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          {users.map((user) => (
            <tr>
              <td>{user.id}</td>
              <td>{user.name}</td>
              <td>{user.email}</td>
              <td>{user.isAdmin}</td>
              <td><Link to={"users/" + user.id} className="btn">Edit</Link></td>
              <td><Link to={"users/" + user.id} className="btn btn-primary">Delete</Link></td>
            </tr>
          ))}
        </tbody>
      </Table>
      <Button onClick={addUser}></Button>
    </Container>
  );
}

function addUser() {

}


interface UserListState {
  users: User[];
  loading: boolean;
}

// export class UserList extends React.Component<any, UserListState> {
//   constructor(props: any) {
//     super(props);
//     this.state = {
//       users: [],
//       loading: true,
//     };
//   }

//   componentDidMount() {
//     this.getUsers();
//   }

//   addUser() {

//   }

//   render() {
//     return (
//       <Container>
//         <Table striped bordered>
//           <thead>
//             <tr>
//               <th>Id</th>
//               <th>Name</th>
//               <th>Email</th>
//               <th>Admin</th>
//               <th></th>
//               <th></th>
//             </tr>
//           </thead>
//           <tbody>
//             {this.state.users.map((user) => (
//               <tr>
//                 <td>{user.id}</td>
//                 <td>{user.name}</td>
//                 <td>{user.email}</td>
//                 <td>{user.isAdmin}</td>
//                 <td><Link to={"users/" + user.id} className="btn">Edit</Link></td>
//                 <td><Link to={"users/" + user.id} className="btn btn-primary">Delete</Link></td>
//               </tr>
//             ))}
//           </tbody>
//         </Table>
//         <Button onClick={this.addUser}></Button>
//       </Container>
//     );
//   }

//   async getUsers() {
//     const response = await useGetUserList();
//     this.setState({ users: response, loading: false });
//   }
// }
