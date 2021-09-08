import React from "react";
import { useContext } from "react";
import { TeamContext } from "../../Data/Contexts/TeamsContext";
import { CustomPicker } from "react-native-custom-picker";
import { Text, View } from "react-native";

type Props = {
  id: number | undefined;
  onTeamChanged: (e: number) => void;
};

type Options = {
  value: number;
  label: string;
};

export const TeamSelector: React.FC<Props> = ({ id, onTeamChanged }) => {
  const teamContext = useContext(TeamContext);

  const options = (): Options[] => {
    let opts: Options[] = [];
    teamContext.teams.forEach((team) => {
      opts.push({ value: team.id!, label: team.name! });
    });
    return opts;
  };

  const renderField = (settings: any) => {
    const { selectedItem, defaultText, getLabel } = settings;
    return (
      <View>
        <View>
          {!selectedItem && <Text>{defaultText}</Text>}
          {selectedItem && (
            <View>
              <Text>{getLabel(selectedItem)}</Text>
            </View>
          )}
        </View>
      </View>
    );
  };

  const renderOption = (settings: any) => {
    const { item, getLabel } = settings;
    return (
      <View>
        <View>
          <Text>{getLabel(item)}</Text>
        </View>
      </View>
    );
  };

  return (
    <CustomPicker
      value={options().find((o) => o.value === id)}
      getLabel={(item) => item.label}
      fieldTemplate={renderField}
      optionTemplate={renderOption}
      options={options()}
      onValueChange={(value) => onTeamChanged(value)}
    ></CustomPicker>
  );
};
