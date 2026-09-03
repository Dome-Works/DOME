import type { Device } from '../types/devices'

type DeviceTabsProps = {
  devices: Device[]
  selectedDeviceName: string
  onSelect: (deviceName: string) => void
}

export function DeviceTabs({
  devices,
  selectedDeviceName,
  onSelect,
}: DeviceTabsProps) {
  return (
    <div className="device-tabs" role="tablist" aria-label="Devices">
      {devices.map((device) => {
        const selected = device.name === selectedDeviceName

        return (
          <button
            key={device.name}
            type="button"
            role="tab"
            aria-selected={selected}
            className={
              selected ? 'device-tab device-tab-selected' : 'device-tab'
            }
            onClick={() => onSelect(device.name)}
          >
            {device.name}
          </button>
        )
      })}
    </div>
  )
}
