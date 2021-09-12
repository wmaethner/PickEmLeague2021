import React, { useEffect, useState } from "react";
import { useContext } from "react";
import { Label } from "reactstrap";
import { TeamContext } from "../../Data/Contexts/TeamsContext";
import { useGetUserImage } from "../../Data/Images/useGetUserImage";
import { Image } from "react-bootstrap";
import DefaultLogo from "../../Assets/DefaultLogo.jpg";

type Props = {
  userId: number | undefined;
};

export const ProfilePicture: React.FC<Props> = ({ userId }) => {
  const [image, setImage] = useState<string | null>("");

  function parseImage(
    data: string | null | undefined,
    type: string | null | undefined
  ): string {
    // return "data:image/" + type + ";base64," + data;
    return "data:image/jpeg;base64," + data;
  }

  useEffect(() => {
    async function GetUser() {
      let imageSrc = await useGetUserImage(userId!);
      setImage(imageSrc ? "data:image/jpeg;base64," + imageSrc : DefaultLogo);
    }
    GetUser();
  }, [userId]);

  return image === "" ? (
    <div></div>
  ) : (
    <Image src={image!} width={75} height={75} />
  );
};
