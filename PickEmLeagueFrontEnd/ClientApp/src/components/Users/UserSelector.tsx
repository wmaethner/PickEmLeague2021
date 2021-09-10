import React, { useEffect, useState } from "react";
import { Button, Row } from 'reactstrap';
import '../../../node_modules/bootstrap/dist/css/bootstrap.min.css';
import { User } from "../../Apis";
import { useGetAllUsers } from "../../Data/User/userGetUserAll";

type Props = {
    user: User;
    setUser: (user: User) => void;
};

export function UserSelector(props: Props) {
    const [users, setUsers] = useState<User[]>([]);

    useEffect(() => {
        (async () => {
            await GetUsers();
        })();
    }, []);

    const GetUsers = async (): Promise<void> => {
        const users = await useGetAllUsers();
        setUsers(users);
    };


    return (
        <Row className="flex justify-content-center">
            {users.map((user) => ( 
                <Button color={user.id == props.user.id ? "primary" : "secondary"} onClick={() => props.setUser(user)}>{user.name}</Button>
            ))}
        </Row>
    );
}
